using FruitAccounting.Data.Context;
using FruitAccounting.Data.Entities;
using FruitAccounting.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace FruitAccounting.Core.services;

public class PurchaseService
{
    private const string PurchaseAccountKey = "PURCHASE_ACCOUNT_ID";
    private const string CommissionIncomeKey = "COMMISSION_INCOME_ACCOUNT_ID";
    private const string VatavExpensesKey = "VATAV_EXPENSES_ACCOUNT_ID";
    private const string FreightPayableKey = "FREIGHT_PAYABLE_ACCOUNT_ID";
    private const string HamaliPayableKey = "HAMALI_PAYABLE_ACCOUNT_ID"; // reused for Labour - same real-world concept
    private const string PostageRecoveredKey = "POSTAGE_RECOVERED_ACCOUNT_ID";
    private const string TdsPayableKey = "TDS_PAYABLE_ACCOUNT_ID";
    private const string Tds194QRateKey = "TDS_194Q_RATE";
    private const string Tds194QThresholdKey = "TDS_194Q_THRESHOLD";

    private readonly IDbContextFactory<FruitAccountingContext> _contextFactory;

    public PurchaseService(IDbContextFactory<FruitAccountingContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<PurchaseBill>> GetAllPurchaseBillsAsync(long financialYearId)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.PurchaseBills
            .AsNoTracking()
            .Include(p => p.Supplier)
            .Include(p => p.PurchaseBillItems).ThenInclude(i => i.Item)
            .Include(p => p.PurchaseBillItems).ThenInclude(i => i.Lot)
            .Where(p => p.FinancialYearId == financialYearId)
            .OrderByDescending(p => p.BillNo)
            .ToListAsync();
    }

    public async Task<int> GetNextBillNoAsync(long financialYearId)
    {
        using var context = _contextFactory.CreateDbContext();
        var max = await context.PurchaseBills
            .Where(p => p.FinancialYearId == financialYearId)
            .MaxAsync(p => (int?)p.BillNo);
        return (max ?? 0) + 1;
    }

    public class PurchaseBillItemInput
    {
        public long ItemId { get; set; }
        public string? Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal? Weight { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public int LotNo { get; set; }
        public string? CrateInfo { get; set; }
    }

    public class PurchaseBillInput
    {
        public DateOnly BillDate { get; set; }
        public long SupplierId { get; set; }
        public PurchaseMode Mode { get; set; }
        public string ChallanNo { get; set; } = null!;
        public string? TruckNo { get; set; }
        public string? Mark { get; set; }
        public string? DeliveryPerson { get; set; }
        public long? AmanatPartyId { get; set; }
        public long? CratePartyId { get; set; }
        public decimal CommissionPct { get; set; }
        public decimal MarketFeePct { get; set; }
        public decimal FreightRate { get; set; }
        public decimal LabourRate { get; set; }
        public decimal Postage { get; set; }
        public decimal PackingMaterial { get; set; }
        public decimal ColdStore { get; set; }
        public decimal VatavPct { get; set; }
        public decimal DdCharge { get; set; }
        public decimal Inam { get; set; }
        public decimal OtherDeduction { get; set; }
        public string? Remarks { get; set; }
        public long FinancialYearId { get; set; }
        public long? CreatedBy { get; set; }
        public List<PurchaseBillItemInput> Items { get; set; } = new();
    }

    private class ComputedTotals
    {
        public decimal GrossAmount;
        public decimal CommissionAmount;
        public decimal MarketFee;
        public decimal Freight;
        public decimal Labour;
        public decimal Vatav;
        public decimal TdsAmount;
        public decimal TdsRate;
        public decimal CumulativeBefore;
        public decimal TaxableExcess;
        public decimal NetAmount;
    }

    public async Task<(bool success, string message)> CreatePurchaseBillAsync(PurchaseBillInput input)
    {
        using var context = _contextFactory.CreateDbContext();
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var (valid, validationMessage) = Validate(input);
            if (!valid)
                return (false, validationMessage);

            var totals = await ComputeTotalsAsync(context, input, excludingPurchaseBillId: null);

            var nextNo = await context.PurchaseBills
                .Where(p => p.FinancialYearId == input.FinancialYearId)
                .MaxAsync(p => (int?)p.BillNo) ?? 0;
            nextNo++;

            var bill = new PurchaseBill
            {
                FinancialYearId = input.FinancialYearId,
                BillNo = nextNo,
                BillDate = input.BillDate,
                SupplierId = input.SupplierId,
                Mode = input.Mode,
                ChallanNo = input.ChallanNo,
                TruckNo = input.TruckNo,
                Mark = input.Mark,
                DeliveryPerson = input.DeliveryPerson,
                AmanatPartyId = input.AmanatPartyId,
                CratePartyId = input.CratePartyId,
                GrossAmount = totals.GrossAmount,
                CommissionPct = input.CommissionPct,
                CommissionAmount = totals.CommissionAmount,
                MarketFeePct = input.MarketFeePct,
                MarketFee = totals.MarketFee,
                FreightRate = input.FreightRate,
                Freight = totals.Freight,
                LabourRate = input.LabourRate,
                Labour = totals.Labour,
                Postage = input.Postage,
                PackingMaterial = input.PackingMaterial,
                ColdStore = input.ColdStore,
                VatavPct = input.VatavPct,
                Vatav = totals.Vatav,
                DdCharge = input.DdCharge,
                Inam = input.Inam,
                OtherDeduction = input.OtherDeduction,
                TdsPct = totals.TdsRate,
                TdsAmount = totals.TdsAmount,
                NetAmount = totals.NetAmount,
                Remarks = input.Remarks,
                CreatedBy = input.CreatedBy,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            context.PurchaseBills.Add(bill);
            await context.SaveChangesAsync();

            await AddItemsAndLotsAsync(context, bill, input);

            if (totals.TdsAmount != 0)
            {
                context.TdsPurchaseDeductions.Add(new TdsPurchaseDeduction
                {
                    FinancialYearId = input.FinancialYearId,
                    SupplierId = input.SupplierId,
                    PurchaseBillId = bill.PurchaseBillId,
                    CumulativeBefore = totals.CumulativeBefore,
                    TaxableExcess = totals.TaxableExcess,
                    TdsRate = totals.TdsRate,
                    TdsAmount = totals.TdsAmount,
                    DeductedAt = DateTime.UtcNow
                });
            }

            var (postOk, postMessage) = await BuildAndAddLedgerEntriesAsync(context, bill);
            if (!postOk)
            {
                await transaction.RollbackAsync();
                return (false, postMessage);
            }

            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return (true, $"Purchase Bill #{nextNo} created successfully");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return (false, $"Error creating Purchase Bill: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> UpdatePurchaseBillAsync(long purchaseBillId, PurchaseBillInput input)
    {
        using var context = _contextFactory.CreateDbContext();
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var bill = await context.PurchaseBills
                .Include(p => p.PurchaseBillItems)
                .FirstOrDefaultAsync(p => p.PurchaseBillId == purchaseBillId);
            if (bill == null)
                return (false, "Purchase Bill not found");

            var (valid, validationMessage) = Validate(input);
            if (!valid)
                return (false, validationMessage);

            var totals = await ComputeTotalsAsync(context, input, excludingPurchaseBillId: purchaseBillId);

            bill.BillDate = input.BillDate;
            bill.SupplierId = input.SupplierId;
            bill.Mode = input.Mode;
            bill.ChallanNo = input.ChallanNo;
            bill.TruckNo = input.TruckNo;
            bill.Mark = input.Mark;
            bill.DeliveryPerson = input.DeliveryPerson;
            bill.AmanatPartyId = input.AmanatPartyId;
            bill.CratePartyId = input.CratePartyId;
            bill.GrossAmount = totals.GrossAmount;
            bill.CommissionPct = input.CommissionPct;
            bill.CommissionAmount = totals.CommissionAmount;
            bill.MarketFeePct = input.MarketFeePct;
            bill.MarketFee = totals.MarketFee;
            bill.FreightRate = input.FreightRate;
            bill.Freight = totals.Freight;
            bill.LabourRate = input.LabourRate;
            bill.Labour = totals.Labour;
            bill.Postage = input.Postage;
            bill.PackingMaterial = input.PackingMaterial;
            bill.ColdStore = input.ColdStore;
            bill.VatavPct = input.VatavPct;
            bill.Vatav = totals.Vatav;
            bill.DdCharge = input.DdCharge;
            bill.Inam = input.Inam;
            bill.OtherDeduction = input.OtherDeduction;
            bill.TdsPct = totals.TdsRate;
            bill.TdsAmount = totals.TdsAmount;
            bill.NetAmount = totals.NetAmount;
            bill.Remarks = input.Remarks;
            bill.UpdatedAt = DateTime.UtcNow;

            context.PurchaseBillItems.RemoveRange(bill.PurchaseBillItems);
            await context.SaveChangesAsync();

            await AddItemsAndLotsAsync(context, bill, input);

            var oldDeductions = await context.TdsPurchaseDeductions
                .Where(d => d.PurchaseBillId == purchaseBillId)
                .ToListAsync();
            context.TdsPurchaseDeductions.RemoveRange(oldDeductions);

            if (totals.TdsAmount != 0)
            {
                context.TdsPurchaseDeductions.Add(new TdsPurchaseDeduction
                {
                    FinancialYearId = input.FinancialYearId,
                    SupplierId = input.SupplierId,
                    PurchaseBillId = bill.PurchaseBillId,
                    CumulativeBefore = totals.CumulativeBefore,
                    TaxableExcess = totals.TaxableExcess,
                    TdsRate = totals.TdsRate,
                    TdsAmount = totals.TdsAmount,
                    DeductedAt = DateTime.UtcNow
                });
            }

            var oldEntries = await context.LedgerEntries
                .Where(e => e.VoucherId == purchaseBillId && e.VoucherType == VoucherType.PurchaseBill)
                .ToListAsync();
            context.LedgerEntries.RemoveRange(oldEntries);

            await context.SaveChangesAsync();

            var (postOk, postMessage) = await BuildAndAddLedgerEntriesAsync(context, bill);
            if (!postOk)
            {
                await transaction.RollbackAsync();
                return (false, postMessage);
            }

            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return (true, "Purchase Bill updated successfully");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return (false, $"Error updating Purchase Bill: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> DeletePurchaseBillAsync(long purchaseBillId)
    {
        using var context = _contextFactory.CreateDbContext();
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var bill = await context.PurchaseBills
                .Include(p => p.PurchaseBillItems)
                .Include(p => p.PaymentAllocations)
                .FirstOrDefaultAsync(p => p.PurchaseBillId == purchaseBillId);
            if (bill == null)
                return (false, "Purchase Bill not found");

            if (bill.PaymentAllocations.Any())
                return (false, "Cannot delete a Purchase Bill that has Payments allocated against it");

            var deductions = await context.TdsPurchaseDeductions
                .Where(d => d.PurchaseBillId == purchaseBillId)
                .ToListAsync();
            context.TdsPurchaseDeductions.RemoveRange(deductions);

            var entries = await context.LedgerEntries
                .Where(e => e.VoucherId == purchaseBillId && e.VoucherType == VoucherType.PurchaseBill)
                .ToListAsync();
            context.LedgerEntries.RemoveRange(entries);

            context.PurchaseBillItems.RemoveRange(bill.PurchaseBillItems);
            context.PurchaseBills.Remove(bill);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return (true, "Purchase Bill deleted successfully");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return (false, $"Error deleting Purchase Bill: {ex.Message}");
        }
    }

    // Lets the UI show a live TDS estimate as the bill is being entered, using the same 194Q
    // logic that actually runs at save time.
    public async Task<(decimal rate, decimal tdsAmount)> PreviewTdsAsync(long supplierId, long financialYearId, decimal grossAmount, DateOnly billDate, long? excludingPurchaseBillId)
    {
        using var context = _contextFactory.CreateDbContext();
        var (rate, _, _, tdsAmount) = await Compute194QAsync(context, supplierId, financialYearId, grossAmount, billDate, excludingPurchaseBillId);
        return (rate, tdsAmount);
    }

    private static (bool valid, string message) Validate(PurchaseBillInput input)
    {
        if (string.IsNullOrWhiteSpace(input.ChallanNo))
            return (false, "Challan No. is required");
        if (input.Items.Count == 0)
            return (false, "At least one item line is required");
        foreach (var item in input.Items)
        {
            if (item.Quantity <= 0)
                return (false, "Every item line needs a Qty greater than zero");
            if (item.Rate <= 0)
                return (false, "Every item line needs a Rate greater than zero");
            if (item.LotNo <= 0)
                return (false, "Every item line needs a Lot No.");
        }
        return (true, "");
    }

    // Trading and Direct modes are straight cash purchases - no commission/vatav/freight/labour
    // deducted, Net Amount equals Gross exactly. WithoutCommission still deducts commission using
    // the same math as WithCommission - the "without" only affects the UI (the amount stays
    // hidden/blank on screen), not the actual ledger deduction.
    private async Task<ComputedTotals> ComputeTotalsAsync(FruitAccountingContext context, PurchaseBillInput input, long? excludingPurchaseBillId)
    {
        var totals = new ComputedTotals
        {
            GrossAmount = input.Items.Sum(i => i.Amount)
        };

        bool noDeductions = input.Mode == PurchaseMode.Trading || input.Mode == PurchaseMode.Direct;
        var totalQty = input.Items.Sum(i => i.Quantity);

        if (!noDeductions)
        {
            totals.CommissionAmount = Math.Round(totals.GrossAmount * input.CommissionPct / 100, 2);
            totals.MarketFee = Math.Round(totals.GrossAmount * input.MarketFeePct / 100, 2);
            totals.Freight = Math.Round(totalQty * input.FreightRate, 2);
            totals.Labour = Math.Round(totalQty * input.LabourRate, 2);
            totals.Vatav = Math.Round(totals.GrossAmount * input.VatavPct / 100, 2);
        }

        var (tdsRate, cumulativeBefore, taxableExcess, tdsAmount) = noDeductions
            ? (0m, 0m, 0m, 0m)
            : await Compute194QAsync(context, input.SupplierId, input.FinancialYearId, totals.GrossAmount, input.BillDate, excludingPurchaseBillId);
        totals.TdsRate = tdsRate;
        totals.CumulativeBefore = cumulativeBefore;
        totals.TaxableExcess = taxableExcess;
        totals.TdsAmount = tdsAmount;

        totals.NetAmount = noDeductions
            ? totals.GrossAmount
            : totals.GrossAmount - totals.CommissionAmount - totals.MarketFee - totals.Freight - totals.Labour
                - input.Postage - input.PackingMaterial - input.ColdStore - totals.Vatav
                - input.DdCharge - input.Inam - input.OtherDeduction - totals.TdsAmount;

        return totals;
    }

    // Section 194Q: TDS applies once cumulative purchases from this supplier in this financial year
    // cross the threshold. The bill that crosses it is taxed only on the excess over the threshold;
    // every bill after that is taxed on its full value.
    private async Task<(decimal rate, decimal cumulativeBefore, decimal taxableExcess, decimal tdsAmount)> Compute194QAsync(
        FruitAccountingContext context, long supplierId, long financialYearId, decimal thisGrossAmount, DateOnly billDate, long? excludingPurchaseBillId)
    {
        var rateParam = await context.SystemParameters.AsNoTracking().FirstOrDefaultAsync(p => p.ParameterKey == Tds194QRateKey);
        var thresholdParam = await context.SystemParameters.AsNoTracking().FirstOrDefaultAsync(p => p.ParameterKey == Tds194QThresholdKey);

        var rate = rateParam != null && decimal.TryParse(rateParam.ParameterValue, out var r) ? r : 0;
        var threshold = thresholdParam != null && decimal.TryParse(thresholdParam.ParameterValue, out var t) ? t : decimal.MaxValue;

        var priorBillsQuery = context.PurchaseBills
            .Where(p => p.SupplierId == supplierId && p.FinancialYearId == financialYearId
                && (p.BillDate < billDate || (p.BillDate == billDate && p.BillNo < 0)));
        // BillNo isn't known yet for a bill being created, and BillNo < 0 never matches, so ties on
        // the same date are resolved by exclusion below rather than ordering - acceptable for now.
        if (excludingPurchaseBillId.HasValue)
            priorBillsQuery = priorBillsQuery.Where(p => p.PurchaseBillId != excludingPurchaseBillId.Value);

        var cumulativeBefore = await priorBillsQuery.SumAsync(p => (decimal?)p.GrossAmount) ?? 0;

        var newCumulative = cumulativeBefore + thisGrossAmount;
        var taxableExcess = Math.Max(0, newCumulative - Math.Max(cumulativeBefore, threshold));
        var tdsAmount = Math.Round(taxableExcess * rate / 100, 2);

        return (rate, cumulativeBefore, taxableExcess, tdsAmount);
    }

    private static async Task AddItemsAndLotsAsync(FruitAccountingContext context, PurchaseBill bill, PurchaseBillInput input)
    {
        foreach (var itemInput in input.Items)
        {
            var lot = await context.Lots.FirstOrDefaultAsync(l => l.FinancialYearId == input.FinancialYearId && l.LotNo == itemInput.LotNo);
            if (lot == null)
            {
                lot = new Lot
                {
                    FinancialYearId = input.FinancialYearId,
                    LotNo = itemInput.LotNo,
                    SupplierId = input.SupplierId,
                    ItemId = itemInput.ItemId,
                    ReceivedDate = input.BillDate,
                    IsClosed = false
                };
                context.Lots.Add(lot);
                await context.SaveChangesAsync();
            }

            context.PurchaseBillItems.Add(new PurchaseBillItem
            {
                PurchaseBillId = bill.PurchaseBillId,
                LotId = lot.LotId,
                ItemId = itemInput.ItemId,
                Description = itemInput.Description,
                Quantity = itemInput.Quantity,
                Weight = itemInput.Weight,
                GrossRate = itemInput.Rate,
                NetRate = itemInput.Rate,
                Amount = itemInput.Amount,
                CrateInfo = itemInput.CrateInfo
            });
        }

        await context.SaveChangesAsync();
    }

    private async Task<(bool success, string message)> BuildAndAddLedgerEntriesAsync(FruitAccountingContext context, PurchaseBill bill)
    {
        var entries = new List<LedgerEntry>();

        var purchaseAccountId = await GetConfiguredAccountIdAsync(context, PurchaseAccountKey);
        if (purchaseAccountId == null)
            return (false, "No Purchase account is configured (system_parameters: PURCHASE_ACCOUNT_ID)");

        // The full gross cost of goods purchased
        entries.Add(new LedgerEntry
        {
            FinancialYearId = bill.FinancialYearId,
            EntryDate = bill.BillDate,
            AccountId = purchaseAccountId.Value,
            Debit = bill.GrossAmount,
            Credit = 0,
            VoucherId = bill.PurchaseBillId,
            VoucherType = VoucherType.PurchaseBill,
            Narration = $"Purchase Bill #{bill.BillNo}",
            ContraAccountId = bill.SupplierId,
            CreatedAt = DateTime.UtcNow
        });

        // What's actually owed to the supplier after every deduction
        entries.Add(new LedgerEntry
        {
            FinancialYearId = bill.FinancialYearId,
            EntryDate = bill.BillDate,
            AccountId = bill.SupplierId,
            Debit = 0,
            Credit = bill.NetAmount,
            VoucherId = bill.PurchaseBillId,
            VoucherType = VoucherType.PurchaseBill,
            Narration = $"Purchase Bill #{bill.BillNo}",
            ContraAccountId = purchaseAccountId.Value,
            CreatedAt = DateTime.UtcNow
        });

        async Task<(bool ok, string msg)> AddDeductionAsync(decimal amount, string key, string missingMessage, string narration)
        {
            if (amount == 0) return (true, "");
            var accountId = await GetConfiguredAccountIdAsync(context, key);
            if (accountId == null) return (false, missingMessage);
            entries.Add(new LedgerEntry
            {
                FinancialYearId = bill.FinancialYearId,
                EntryDate = bill.BillDate,
                AccountId = accountId.Value,
                Debit = 0,
                Credit = amount,
                VoucherId = bill.PurchaseBillId,
                VoucherType = VoucherType.PurchaseBill,
                Narration = narration,
                ContraAccountId = bill.SupplierId,
                CreatedAt = DateTime.UtcNow
            });
            return (true, "");
        }

        var (ok1, msg1) = await AddDeductionAsync(bill.CommissionAmount, CommissionIncomeKey,
            "Commission amount computed, but no Commission Income account is configured (system_parameters: COMMISSION_INCOME_ACCOUNT_ID)",
            $"Commission on Purchase Bill #{bill.BillNo}");
        if (!ok1) return (false, msg1);

        var (ok2, msg2) = await AddDeductionAsync(bill.Vatav ?? 0, VatavExpensesKey,
            "Vatav amount computed, but no Vatav Expenses account is configured (system_parameters: VATAV_EXPENSES_ACCOUNT_ID)",
            $"Vatav on Purchase Bill #{bill.BillNo}");
        if (!ok2) return (false, msg2);

        var (ok3, msg3) = await AddDeductionAsync(bill.Freight ?? 0, FreightPayableKey,
            "Freight amount computed, but no Freight Payable account is configured (system_parameters: FREIGHT_PAYABLE_ACCOUNT_ID)",
            $"Freight on Purchase Bill #{bill.BillNo}");
        if (!ok3) return (false, msg3);

        var (ok4, msg4) = await AddDeductionAsync(bill.Labour ?? 0, HamaliPayableKey,
            "Labour amount computed, but no Labour/Hamali Payable account is configured (system_parameters: HAMALI_PAYABLE_ACCOUNT_ID)",
            $"Labour on Purchase Bill #{bill.BillNo}");
        if (!ok4) return (false, msg4);

        var (ok5, msg5) = await AddDeductionAsync(bill.Postage ?? 0, PostageRecoveredKey,
            "Postage amount entered, but no Postage Recovered account is configured (system_parameters: POSTAGE_RECOVERED_ACCOUNT_ID)",
            $"Postage on Purchase Bill #{bill.BillNo}");
        if (!ok5) return (false, msg5);

        if (bill.TdsAmount != 0)
        {
            var (ok6, msg6) = await AddDeductionAsync(bill.TdsAmount ?? 0, TdsPayableKey,
                "TDS amount computed, but no TDS Payable account is configured (system_parameters: TDS_PAYABLE_ACCOUNT_ID)",
                $"194Q TDS on Purchase Bill #{bill.BillNo}");
            if (!ok6) return (false, msg6);
        }

        // Packing Material, Cold Store, Bank Charges, Inam, Other Deduction, and Market Fee are
        // rarely-used legacy fields with no confirmed real example to design a dedicated account
        // against - if entered, they still need to balance the voucher, so they're recovered
        // through the same Postage account for now rather than inventing five near-empty GL
        // accounts with nothing to validate them against.
        var miscTotal = (bill.PackingMaterial ?? 0) + (bill.ColdStore ?? 0) + (bill.DdCharge ?? 0) + (bill.Inam ?? 0) + (bill.OtherDeduction ?? 0) + (bill.MarketFee ?? 0);
        var (ok7, msg7) = await AddDeductionAsync(miscTotal, PostageRecoveredKey,
            "Packing/Store/Bank/Inam/Other/Market Fee amount entered, but no Postage Recovered account is configured (system_parameters: POSTAGE_RECOVERED_ACCOUNT_ID)",
            $"Packing/Store/Bank/Inam/Other/Market Fee on Purchase Bill #{bill.BillNo}");
        if (!ok7) return (false, msg7);

        context.LedgerEntries.AddRange(entries);
        return (true, "");
    }

    private async Task<long?> GetConfiguredAccountIdAsync(FruitAccountingContext context, string key)
    {
        var param = await context.SystemParameters.AsNoTracking().FirstOrDefaultAsync(p => p.ParameterKey == key);
        if (param == null) return null;
        return long.TryParse(param.ParameterValue, out var id) ? id : null;
    }
}
