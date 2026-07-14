using FruitAccounting.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FruitAccounting.Core.services;

// Section 194Q: TDS applies once a supplier's cumulative "purchase of goods" value in a financial
// year crosses the ₹50L threshold. Payments and Purchase Bills are two views of the SAME
// underlying value stream (advance now, bill later, or bill now, settlement payment later) - they
// must NOT be summed together, or a purchase already covered by a prior advance (or a payment
// settling an already-billed purchase) gets taxed twice. Instead, the true cumulative at any point
// is MAX(total bills so far, total payments so far): whichever channel has recognized more value
// IS the cumulative, and the other channel's transactions are presumed to be settling against it
// rather than adding fresh value. A transaction only pushes the cumulative up (and becomes
// taxable) to the extent it grows the running max beyond what the other channel already covers.
// Shared by PurchaseService and PaymentService so both sides see the same combined running total.
public class Tds194QService
{
    private const string Tds194QRateKey = "TDS_194Q_RATE";
    private const string Tds194QThresholdKey = "TDS_194Q_THRESHOLD";

    public async Task<(decimal rate, decimal cumulativeBefore, decimal taxableExcess, decimal tdsAmount)> ComputeAsync(
        FruitAccountingContext context, long supplierId, long financialYearId, decimal thisAmount, DateOnly transactionDate,
        bool isPayment, long? excludingPurchaseBillId, long? excludingPaymentId)
    {
        var rateParam = await context.SystemParameters.AsNoTracking().FirstOrDefaultAsync(p => p.ParameterKey == Tds194QRateKey);
        var thresholdParam = await context.SystemParameters.AsNoTracking().FirstOrDefaultAsync(p => p.ParameterKey == Tds194QThresholdKey);

        var rate = rateParam != null && decimal.TryParse(rateParam.ParameterValue, out var r) ? r : 0;
        var threshold = thresholdParam != null && decimal.TryParse(thresholdParam.ParameterValue, out var t) ? t : decimal.MaxValue;

        // Same-day collisions between a bill and a payment are the norm now (not a rare tie), so
        // any DateOnly-only record on the same calendar date as this transaction must still count
        // as prior - it already exists in the DB, so it necessarily happened before this one (a
        // brand-new record isn't saved yet; an existing record being edited is excluded below by
        // its own ID instead). No tie-break needed - "<=" is correct and unambiguous either way.
        var priorBillsQuery = context.PurchaseBills
            .Where(p => p.SupplierId == supplierId && p.FinancialYearId == financialYearId && p.BillDate <= transactionDate);
        if (excludingPurchaseBillId.HasValue)
            priorBillsQuery = priorBillsQuery.Where(p => p.PurchaseBillId != excludingPurchaseBillId.Value);
        var priorBillsSum = await priorBillsQuery.SumAsync(p => (decimal?)p.GrossAmount) ?? 0;

        var priorPaymentsQuery = context.Payments
            .Where(p => p.AccountId == supplierId && p.FinancialYearId == financialYearId && !p.IsFreightPayment
                && p.PaymentDate <= transactionDate);
        if (excludingPaymentId.HasValue)
            priorPaymentsQuery = priorPaymentsQuery.Where(p => p.PaymentId != excludingPaymentId.Value);
        var priorPaymentsSum = await priorPaymentsQuery.SumAsync(p => (decimal?)p.Amount) ?? 0;

        var cumulativeBefore = Math.Max(priorBillsSum, priorPaymentsSum);
        var newCumulative = isPayment
            ? Math.Max(priorBillsSum, priorPaymentsSum + thisAmount)
            : Math.Max(priorBillsSum + thisAmount, priorPaymentsSum);
        var taxableExcess = Math.Max(0, newCumulative - Math.Max(cumulativeBefore, threshold));
        var tdsAmount = Math.Round(taxableExcess * rate / 100, 2);

        return (rate, cumulativeBefore, taxableExcess, tdsAmount);
    }
}
