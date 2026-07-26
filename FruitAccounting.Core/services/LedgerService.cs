using FruitAccounting.Data.Context;
using FruitAccounting.Data.Entities;
using FruitAccounting.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace FruitAccounting.Core.services;

public class LedgerService
{
    private readonly IDbContextFactory<FruitAccountingContext> _contextFactory;

    public LedgerService(IDbContextFactory<FruitAccountingContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public class LedgerRow
    {
        public DateOnly Date { get; set; }

        // May contain a "\n" for a two-line detail (Receipt: contra cash/bank account on line 1,
        // Vatav/Amount breakdown on line 2).
        public string Detail { get; set; } = "";

        // Cheque number when the voucher was paid by cheque, otherwise the user who entered it.
        public string ChequeOrUser { get; set; } = "";

        public decimal Debit { get; set; }
        public decimal Credit { get; set; }

        // Signed running balance as of this row: positive = Dr, negative = Cr.
        public decimal RunningBalance { get; set; }

        // Short voucher-type code shown in its own grid column, e.g. "CR"/"BR"/"CP"/"BP"/"S"/"P"/"JV".
        public string VoucherCode { get; set; } = "";

        // Drill-down keys, so the UI can open the originating voucher screen on this row.
        public VoucherType VoucherType { get; set; }
        // Meaning depends on VoucherType: PurchaseBillId / SaleId / ReceiptId / PaymentId / JournalVoucherId.
        public long VoucherId { get; set; }
        // Sales only - SalesForm navigates by InvNo (a voucher groups several Sale rows), not SaleId.
        public int? SalesInvNo { get; set; }
        // Receipt/Payment only - which book ('C'ash or 'B'ank) the originating ReceiptForm/PaymentForm
        // instance must be opened in, since those forms are book-type-scoped.
        public char? BookType { get; set; }
    }

    public class LedgerResult
    {
        public Account Account { get; set; } = null!;

        // Signed balance carried into the period (opening balance for the FY plus any
        // entries before FromDate) - positive = Dr, negative = Cr.
        public decimal OpeningBalance { get; set; }

        public List<LedgerRow> Rows { get; set; } = new();
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal ClosingBalance { get; set; }
    }

    /// Ledger for several accounts at once (Account -> Reports -> Ledger -> All / Group Wise) -
    /// one full LedgerResult per account, in the order accountIds was given, skipping accounts
    /// with no activity in the period (no rows and a zero opening balance). Runs GetLedgerAsync
    /// per account rather than a single batched query - simplest correct approach; revisit if the
    /// "All accounts" case becomes a performance problem once this is wired to real report output.
    public async Task<List<LedgerResult>> GetLedgerForAccountsAsync(IEnumerable<long> accountIds,
        long financialYearId, DateOnly fromDate, DateOnly toDate)
    {
        var results = new List<LedgerResult>();
        foreach (var accountId in accountIds)
        {
            var result = await GetLedgerAsync(accountId, financialYearId, fromDate, toDate);
            if (result.Rows.Count > 0 || result.OpeningBalance != 0)
                results.Add(result);
        }
        return results;
    }

    /// <param name="voucherTypeFilter">
    /// When set, only rows of this VoucherType are returned (and counted in TotalDebit/TotalCredit) -
    /// e.g. "show me just the Purchase entries on this party's ledger". The Balance column on every
    /// returned row still reflects the account's true running balance (all voucher types folded in,
    /// including the ones hidden by the filter) - it is a view filter on which rows are shown, not a
    /// recalculation of the balance from a narrower transaction set. As a result Opening + TotalDebit -
    /// TotalCredit will not equal ClosingBalance when a filter is active; that's expected.
    /// </param>
    /// <param name="includeOpeningBalance">
    /// When false ("W/o Opening"), the carried-forward balance (AccountOpeningBalance plus any entries
    /// before FromDate) is ignored - OpeningBalance is 0 and the running balance starts fresh from the
    /// first entry in the period, showing period activity in isolation.
    /// </param>
    public async Task<LedgerResult> GetLedgerAsync(long accountId, long financialYearId, DateOnly fromDate, DateOnly toDate,
        VoucherType? voucherTypeFilter = null, bool includeOpeningBalance = true)
    {
        using var context = _contextFactory.CreateDbContext();

        var account = await context.Accounts.AsNoTracking().FirstAsync(a => a.AccountId == accountId);

        decimal periodOpening = 0m;
        if (includeOpeningBalance)
        {
            var opening = await context.AccountOpeningBalances
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.AccountId == accountId && o.FinancialYearId == financialYearId);

            decimal openingSigned = opening == null ? 0m
                : opening.Side == DrCr.Debit ? opening.Amount : -opening.Amount;

            // Entries dated before the requested period roll into the period's opening balance,
            // same as the legacy Crystal ledger reports (Period From is a filter, not the FY start).
            var priorMovement = await context.LedgerEntries
                .AsNoTracking()
                .Where(e => e.AccountId == accountId && e.FinancialYearId == financialYearId && e.EntryDate < fromDate)
                .SumAsync(e => (decimal?)(e.Debit - e.Credit)) ?? 0m;

            periodOpening = openingSigned + priorMovement;
        }

        var entries = await context.LedgerEntries
            .AsNoTracking()
            .Include(e => e.ContraAccount)
            .Where(e => e.AccountId == accountId && e.FinancialYearId == financialYearId
                        && e.EntryDate >= fromDate && e.EntryDate <= toDate)
            .OrderBy(e => e.EntryDate)
            .ThenBy(e => e.LedgerEntryId)
            .ToListAsync();

        // Receipt/Payment/Journal entries need their originating voucher (for the Vatav/Amount
        // breakdown, the cheque number, and who entered it) - LedgerEntry itself doesn't carry
        // those. Batched once up front rather than per-row.
        var receiptVoucherIds = entries.Where(e => e.VoucherType == VoucherType.Receipt)
            .Select(e => e.VoucherId).Distinct().ToList();
        var receiptsById = receiptVoucherIds.Count == 0
            ? new Dictionary<long, Receipt>()
            : await context.Receipts.AsNoTracking()
                .Include(r => r.CreatedByNavigation)
                .Include(r => r.Daybook)
                .Where(r => receiptVoucherIds.Contains(r.ReceiptId))
                .ToDictionaryAsync(r => r.ReceiptId);

        var paymentVoucherIds = entries.Where(e => e.VoucherType == VoucherType.Payment)
            .Select(e => e.VoucherId).Distinct().ToList();
        var paymentsById = paymentVoucherIds.Count == 0
            ? new Dictionary<long, Payment>()
            : await context.Payments.AsNoTracking()
                .Include(p => p.CreatedByNavigation)
                .Include(p => p.Daybook)
                .Where(p => paymentVoucherIds.Contains(p.PaymentId))
                .ToDictionaryAsync(p => p.PaymentId);

        var journalVoucherIds = entries.Where(e => e.VoucherType == VoucherType.Journal)
            .Select(e => e.VoucherId).Distinct().ToList();
        var journalsById = journalVoucherIds.Count == 0
            ? new Dictionary<long, JournalVoucher>()
            : await context.JournalVouchers.AsNoTracking()
                .Include(j => j.CreatedByNavigation)
                .Where(j => journalVoucherIds.Contains(j.JournalVoucherId))
                .ToDictionaryAsync(j => j.JournalVoucherId);

        // Sales quantity is read live from the Sale row rather than LedgerEntry.Quantity, so
        // historical rows posted before this column was populated still show correctly.
        var saleVoucherIds = entries.Where(e => e.VoucherType == VoucherType.SalesBill)
            .Select(e => e.VoucherId).Distinct().ToList();
        var salesById = saleVoucherIds.Count == 0
            ? new Dictionary<long, Sale>()
            : await context.Sales.AsNoTracking()
                .Where(s => saleVoucherIds.Contains(s.SaleId))
                .ToDictionaryAsync(s => s.SaleId);

        var rows = new List<LedgerRow>();
        decimal running = periodOpening;
        decimal totalDebit = 0m, totalCredit = 0m;
        foreach (var e in entries)
        {
            running += e.Debit - e.Credit;

            if (voucherTypeFilter != null && e.VoucherType != voucherTypeFilter)
                continue;

            totalDebit += e.Debit;
            totalCredit += e.Credit;
            var (detail, chequeOrUser) = BuildDetail(e, receiptsById, paymentsById, journalsById, salesById);
            var bookType = e.VoucherType == VoucherType.Receipt && receiptsById.TryGetValue(e.VoucherId, out var rc) ? rc.Daybook.BookType
                : e.VoucherType == VoucherType.Payment && paymentsById.TryGetValue(e.VoucherId, out var pm) ? pm.Daybook.BookType
                : (char?)null;
            rows.Add(new LedgerRow
            {
                Date = e.EntryDate,
                Detail = detail,
                ChequeOrUser = chequeOrUser,
                Debit = e.Debit,
                Credit = e.Credit,
                RunningBalance = running,
                VoucherCode = VoucherCode(e.VoucherType, bookType),
                VoucherType = e.VoucherType,
                VoucherId = e.VoucherId,
                SalesInvNo = e.VoucherType == VoucherType.SalesBill && salesById.TryGetValue(e.VoucherId, out var s) ? s.InvNo : null,
                BookType = bookType
            });
        }

        return new LedgerResult
        {
            Account = account,
            OpeningBalance = periodOpening,
            Rows = rows,
            TotalDebit = totalDebit,
            TotalCredit = totalCredit,
            ClosingBalance = running
        };
    }

    private static string VoucherCode(VoucherType voucherType, char? bookType) => voucherType switch
    {
        VoucherType.Receipt => bookType == 'B' ? "BR" : "CR",
        VoucherType.Payment => bookType == 'B' ? "BP" : "CP",
        VoucherType.SalesBill => "S",
        VoucherType.PurchaseBill => "P",
        VoucherType.Journal => "JV",
        VoucherType.BankEntry => "BE",
        VoucherType.TdsPayment => "TDS",
        VoucherType.OpeningBalance => "OB",
        VoucherType.Crate => "CT",
        VoucherType.ColdStorage => "CS",
        VoucherType.DesavarPurchase => "DP",
        VoucherType.DesavarSale => "DS",
        VoucherType.ImportPurchase => "IP",
        VoucherType.ImportSale => "IS",
        _ => ""
    };

    private static (string Detail, string ChequeOrUser) BuildDetail(LedgerEntry e,
        Dictionary<long, Receipt> receiptsById, Dictionary<long, Payment> paymentsById,
        Dictionary<long, JournalVoucher> journalsById, Dictionary<long, Sale> salesById)
    {
        // Sales: the quantity sold is more useful on a party's ledger than the invoice number.
        if (e.VoucherType == VoucherType.SalesBill && salesById.TryGetValue(e.VoucherId, out var sale))
            return ($"Tot.Qty : {sale.Quantity:0.##}", "");

        if (e.VoucherType == VoucherType.Receipt && receiptsById.TryGetValue(e.VoucherId, out var receipt))
        {
            var detail = $"{ContraDisplay(e)}\n{AmountVatavLine(receipt.TotalSettled, receipt.Vatav)}";
            var chequeOrUser = ChequeOrUser(receipt.ChequeNo, receipt.CreatedByNavigation);
            return (detail, chequeOrUser);
        }

        if (e.VoucherType == VoucherType.Payment && paymentsById.TryGetValue(e.VoucherId, out var payment))
        {
            var detail = $"{ContraDisplay(e)}\n{AmountVatavLine(payment.TotalSettled, payment.Vatav)}";
            var chequeOrUser = ChequeOrUser(payment.ChequeNo, payment.CreatedByNavigation);
            return (detail, chequeOrUser);
        }

        if (e.VoucherType == VoucherType.Journal && journalsById.TryGetValue(e.VoucherId, out var journal))
        {
            // Journal has no cheque concept - always falls back to the entering user.
            var detail = !string.IsNullOrWhiteSpace(e.Narration) ? e.Narration! : (ContraDisplay(e) is { Length: > 0 } c ? c : e.VoucherType.ToString());
            var chequeOrUser = ChequeOrUser(null, journal.CreatedByNavigation);
            return (detail, chequeOrUser);
        }

        if (!string.IsNullOrWhiteSpace(e.Narration)) return (e.Narration!, "");
        if (e.ContraAccount != null) return (e.ContraAccount.Name, "");
        return (e.VoucherType.ToString(), "");
    }

    // Amount is always shown when non-zero; Vatav only tags along when it actually applies -
    // no "Vatav: 0.00" clutter on the common case where no discount was given.
    private static string AmountVatavLine(decimal amount, decimal vatav)
    {
        if (amount == 0 && vatav == 0) return "";
        var parts = new List<string> { $"Amount: {amount:N2}" };
        if (vatav != 0) parts.Add($"Vatav: {vatav:N2}");
        return string.Join("   ", parts);
    }

    private static string ContraDisplay(LedgerEntry e)
    {
        if (e.ContraAccount == null) return "";
        return string.IsNullOrWhiteSpace(e.ContraAccount.BankAccountNo)
            ? e.ContraAccount.Name
            : $"{e.ContraAccount.Name} : {e.ContraAccount.BankAccountNo}";
    }

    private static string ChequeOrUser(string? chequeNo, User? createdBy)
    {
        if (!string.IsNullOrWhiteSpace(chequeNo)) return chequeNo!;
        return createdBy?.DisplayName ?? createdBy?.Username ?? "";
    }
}
