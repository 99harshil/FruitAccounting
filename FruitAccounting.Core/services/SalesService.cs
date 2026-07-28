using FruitAccounting.Data.Context;
using FruitAccounting.Data.Entities;
using FruitAccounting.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace FruitAccounting.Core.services;

// A "Sales voucher" is a Lot's dispersal across multiple buyers entered in one sitting - the
// legacy system (and this schema) has no separate header table for it, just a group of `Sale`
// rows sharing the same (FinancialYearId, InvNo). This service treats that grouping as the
// voucher boundary throughout.
public class SalesService
{
    private const string SalesAccountKey = "SALES_ACCOUNT_ID";
    private const string ApmcPayableKey = "APMC_PAYABLE_ACCOUNT_ID";
    private const string HamaliPayableKey = "HAMALI_PAYABLE_ACCOUNT_ID"; // reused for Labour - same as Purchase Bill

    private readonly IDbContextFactory<FruitAccountingContext> _contextFactory;

    public SalesService(IDbContextFactory<FruitAccountingContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public class SalesVoucher
    {
        public int InvNo { get; set; }
        public DateOnly SaleDate { get; set; }
        public long LotId { get; set; }
        public string? VehNo { get; set; }
        public List<Sale> Lines { get; set; } = new();
    }

    public class SaleLineInput
    {
        public long BuyerId { get; set; }
        public decimal Quantity { get; set; }
        public decimal? Weight { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public decimal LabourRate { get; set; }
        public string? Remarks { get; set; }
    }

    public class SalesVoucherInput
    {
        public long CompanyId { get; set; }
        public long FinancialYearId { get; set; }
        public DateOnly SaleDate { get; set; }
        public long LotId { get; set; }
        public string? VehNo { get; set; }
        public long? CreatedBy { get; set; }
        public List<SaleLineInput> Lines { get; set; } = new();
    }

    public class AvailablePurchaseBillInfo
    {
        public long PurchaseBillId { get; set; }
        public int BillNo { get; set; }
        public DateOnly BillDate { get; set; }
    }

    public class LotSaleInfo
    {
        public long LotId { get; set; }
        public int LotNo { get; set; }
        public long ItemId { get; set; }
        public string ItemName { get; set; } = "";
        public long SupplierId { get; set; }
        public string SupplierName { get; set; } = "";
        public DateOnly PurchaseDate { get; set; }
        public decimal PurchasedQty { get; set; }
        public decimal PurchaseRate { get; set; }
        public string? Mark { get; set; }
        public string? CrateInfo { get; set; }
        public decimal SoldQty { get; set; }
        public decimal BalanceQty { get; set; }
    }

    public async Task<int> GetNextInvNoAsync(long financialYearId)
    {
        using var context = _contextFactory.CreateDbContext();
        var max = await context.Sales
            .Where(s => s.FinancialYearId == financialYearId)
            .MaxAsync(s => (int?)s.InvNo);
        return (max ?? 0) + 1;
    }

    // Bills where at least one item's Lot still has unsold balance - drives the "Pur Bill No."
    // dropdown per the user's rule: only show a bill while purchased qty > sold qty somewhere in it.
    public async Task<List<AvailablePurchaseBillInfo>> GetAvailablePurchaseBillsAsync(long financialYearId)
    {
        using var context = _contextFactory.CreateDbContext();

        var bills = await context.PurchaseBills.AsNoTracking()
            .Include(b => b.PurchaseBillItems)
            .Where(b => b.FinancialYearId == financialYearId)
            .OrderByDescending(b => b.BillNo)
            .ToListAsync();

        var lotIds = bills.SelectMany(b => b.PurchaseBillItems.Select(i => i.LotId)).Distinct().ToList();
        var soldByLot = await context.Sales.AsNoTracking()
            .Where(s => lotIds.Contains(s.LotId))
            .GroupBy(s => s.LotId)
            .Select(g => new { LotId = g.Key, Sold = g.Sum(s => s.Quantity) })
            .ToDictionaryAsync(x => x.LotId, x => x.Sold);

        return bills
            .Where(b => b.PurchaseBillItems.Any(i => i.Quantity > (soldByLot.TryGetValue(i.LotId, out var sold) ? sold : 0)))
            .Select(b => new AvailablePurchaseBillInfo { PurchaseBillId = b.PurchaseBillId, BillNo = b.BillNo, BillDate = b.BillDate })
            .ToList();
    }

    // Lots under a given Purchase Bill that still have unsold balance, with header display fields
    // (item, farmer, purchase date/rate, mark, crate) pulled from the originating PurchaseBillItem.
    public async Task<List<LotSaleInfo>> GetAvailableLotsForPurchaseBillAsync(long purchaseBillId, long financialYearId, int? excludingInvNo = null)
    {
        using var context = _contextFactory.CreateDbContext();

        var bill = await context.PurchaseBills.AsNoTracking()
            .Include(b => b.Supplier)
            .FirstOrDefaultAsync(b => b.PurchaseBillId == purchaseBillId);
        if (bill == null) return new List<LotSaleInfo>();

        var items = await context.PurchaseBillItems.AsNoTracking()
            .Include(i => i.Lot).ThenInclude(l => l.Item)
            .Where(i => i.PurchaseBillId == purchaseBillId)
            .ToListAsync();

        var lotIds = items.Select(i => i.LotId).Distinct().ToList();
        var soldByLot = await GetSoldQtyByLotAsync(context, lotIds, financialYearId, excludingInvNo);
        // A Lot No. can end up reused across more than one Purchase Bill (e.g. the same Lot No.
        // typed into two different bills) - the Lot's true purchased quantity is the sum across
        // every PurchaseBillItem that originated it, not just the row under THIS bill.
        var purchasedByLot = await GetPurchasedQtyByLotAsync(context, lotIds);

        return items
            .Select(i => new LotSaleInfo
            {
                LotId = i.LotId,
                LotNo = i.Lot.LotNo,
                ItemId = i.ItemId,
                ItemName = i.Lot.Item?.Name ?? "",
                SupplierId = bill.SupplierId,
                SupplierName = bill.Supplier?.Name ?? "",
                PurchaseDate = bill.BillDate,
                PurchasedQty = purchasedByLot.TryGetValue(i.LotId, out var p) ? p : i.Quantity,
                PurchaseRate = i.GrossRate,
                Mark = bill.Mark,
                CrateInfo = i.CrateInfo,
                SoldQty = soldByLot.TryGetValue(i.LotId, out var s) ? s : 0,
                BalanceQty = (purchasedByLot.TryGetValue(i.LotId, out var p2) ? p2 : i.Quantity) - (soldByLot.TryGetValue(i.LotId, out var s2) ? s2 : 0)
            })
            .Where(x => x.BalanceQty > 0)
            .ToList();
    }

    // Live balance for a single already-chosen Lot (header display + next-row default suggestion).
    public async Task<LotSaleInfo?> GetLotSaleInfoAsync(long lotId, long financialYearId, int? excludingInvNo = null)
    {
        using var context = _contextFactory.CreateDbContext();

        var item = await context.PurchaseBillItems.AsNoTracking()
            .Include(i => i.Lot).ThenInclude(l => l.Item)
            .Include(i => i.PurchaseBill).ThenInclude(b => b.Supplier)
            .FirstOrDefaultAsync(i => i.LotId == lotId);
        if (item == null) return null;

        var sold = await GetSoldQtyByLotAsync(context, new List<long> { lotId }, financialYearId, excludingInvNo);
        var soldQty = sold.TryGetValue(lotId, out var s) ? s : 0;
        var purchased = await GetPurchasedQtyByLotAsync(context, new List<long> { lotId });
        var purchasedQty = purchased.TryGetValue(lotId, out var p) ? p : item.Quantity;

        return new LotSaleInfo
        {
            LotId = lotId,
            LotNo = item.Lot.LotNo,
            ItemId = item.ItemId,
            ItemName = item.Lot.Item?.Name ?? "",
            SupplierId = item.PurchaseBill.SupplierId,
            SupplierName = item.PurchaseBill.Supplier?.Name ?? "",
            PurchaseDate = item.PurchaseBill.BillDate,
            PurchasedQty = purchasedQty,
            PurchaseRate = item.GrossRate,
            Mark = item.PurchaseBill.Mark,
            CrateInfo = item.CrateInfo,
            SoldQty = soldQty,
            BalanceQty = purchasedQty - soldQty
        };
    }

    // A Lot's true purchased quantity is the sum of every PurchaseBillItem that originated it -
    // normally just one, but see the note above about Lot No. reuse across bills.
    private static async Task<Dictionary<long, decimal>> GetPurchasedQtyByLotAsync(FruitAccountingContext context, List<long> lotIds)
    {
        return await context.PurchaseBillItems.AsNoTracking()
            .Where(i => lotIds.Contains(i.LotId))
            .GroupBy(i => i.LotId)
            .Select(g => new { LotId = g.Key, Purchased = g.Sum(i => i.Quantity) })
            .ToDictionaryAsync(x => x.LotId, x => x.Purchased);
    }

    private static async Task<Dictionary<long, decimal>> GetSoldQtyByLotAsync(
        FruitAccountingContext context, List<long> lotIds, long financialYearId, int? excludingInvNo)
    {
        var query = context.Sales.AsNoTracking().Where(s => lotIds.Contains(s.LotId));
        if (excludingInvNo.HasValue)
            query = query.Where(s => !(s.FinancialYearId == financialYearId && s.InvNo == excludingInvNo.Value));

        return await query
            .GroupBy(s => s.LotId)
            .Select(g => new { LotId = g.Key, Sold = g.Sum(s => s.Quantity) })
            .ToDictionaryAsync(x => x.LotId, x => x.Sold);
    }

    public async Task<List<SalesVoucher>> GetAllSalesVouchersAsync(long financialYearId)
    {
        using var context = _contextFactory.CreateDbContext();
        var allSales = await context.Sales.AsNoTracking()
            .Include(s => s.Buyer)
            .Include(s => s.Lot).ThenInclude(l => l.Item)
            .Include(s => s.Lot).ThenInclude(l => l.Supplier)
            .Where(s => s.FinancialYearId == financialYearId)
            .ToListAsync();

        return allSales
            .GroupBy(s => s.InvNo)
            .Select(g => new SalesVoucher
            {
                InvNo = g.Key,
                SaleDate = g.First().SaleDate,
                LotId = g.First().LotId,
                VehNo = g.First().VehNo,
                Lines = g.OrderBy(s => s.SaleId).ToList()
            })
            .OrderByDescending(v => v.InvNo)
            .ToList();
    }

    public async Task<(bool success, string message)> CreateSalesVoucherAsync(SalesVoucherInput input)
    {
        using var context = _contextFactory.CreateDbContext();
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var (valid, message) = await ValidateAsync(context, input, excludingInvNo: null);
            if (!valid)
                return (false, message);

            var nextInvNo = await context.Sales
                .Where(s => s.FinancialYearId == input.FinancialYearId)
                .MaxAsync(s => (int?)s.InvNo) ?? 0;
            nextInvNo++;

            var apmcPct = await GetCompanyApmcPctAsync(context, input.CompanyId);
            var lot = await context.PurchaseBillItems.FirstOrDefaultAsync(i => i.LotId == input.LotId);

            var sales = BuildSaleRows(input, nextInvNo, apmcPct, lot!.ItemId);
            context.Sales.AddRange(sales);
            await context.SaveChangesAsync();

            //var (postOk, postMessage) = await BuildAndAddLedgerEntriesAsync(context, sales, nextInvNo);
            // Reload sales with Buyer included so AmanatPartyId is accessible in ledger posting
            var reloadedSales = await context.Sales
                .Include(s => s.Buyer)
                .Where(s => s.FinancialYearId == input.FinancialYearId && s.InvNo == nextInvNo)
                .ToListAsync();

            var (postOk, postMessage) = await BuildAndAddLedgerEntriesAsync(context, reloadedSales, nextInvNo);
            if (!postOk)
            {
                await transaction.RollbackAsync();
                return (false, postMessage);
            }

            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return (true, $"Sales #{nextInvNo} created successfully");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return (false, $"Error creating Sales: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> UpdateSalesVoucherAsync(long financialYearId, int invNo, SalesVoucherInput input)
    {
        using var context = _contextFactory.CreateDbContext();
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var (valid, message) = await ValidateAsync(context, input, excludingInvNo: invNo);
            if (!valid)
                return (false, message);

            var oldSales = await context.Sales
                .Where(s => s.FinancialYearId == financialYearId && s.InvNo == invNo)
                .ToListAsync();
            if (oldSales.Count == 0)
                return (false, "Sales voucher not found");

            var oldSaleIds = oldSales.Select(s => s.SaleId).ToList();
            var oldEntries = await context.LedgerEntries
                .Where(e => e.VoucherType == VoucherType.SalesBill && oldSaleIds.Contains(e.VoucherId))
                .ToListAsync();
            context.LedgerEntries.RemoveRange(oldEntries);
            context.Sales.RemoveRange(oldSales);
            await context.SaveChangesAsync();

            var apmcPct = await GetCompanyApmcPctAsync(context, input.CompanyId);
            var lot = await context.PurchaseBillItems.FirstOrDefaultAsync(i => i.LotId == input.LotId);

            var sales = BuildSaleRows(input, invNo, apmcPct, lot!.ItemId);
            context.Sales.AddRange(sales);
            await context.SaveChangesAsync();

            //var (postOk, postMessage) = await BuildAndAddLedgerEntriesAsync(context, sales, invNo);
            // Reload sales with Buyer included so AmanatPartyId is accessible in ledger posting
            var reloadedSales = await context.Sales
                .Include(s => s.Buyer)
                .Where(s => s.FinancialYearId == input.FinancialYearId && s.InvNo == invNo)
                .ToListAsync();

            var (postOk, postMessage) = await BuildAndAddLedgerEntriesAsync(context, reloadedSales, invNo);
            if (!postOk)
            {
                await transaction.RollbackAsync();
                return (false, postMessage);
            }

            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return (true, "Sales voucher updated successfully");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return (false, $"Error updating Sales: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> DeleteSalesVoucherAsync(long financialYearId, int invNo)
    {
        using var context = _contextFactory.CreateDbContext();
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var sales = await context.Sales
                .Where(s => s.FinancialYearId == financialYearId && s.InvNo == invNo)
                .ToListAsync();
            if (sales.Count == 0)
                return (false, "Sales voucher not found");

            var saleIds = sales.Select(s => s.SaleId).ToList();
            var entries = await context.LedgerEntries
                .Where(e => e.VoucherType == VoucherType.SalesBill && saleIds.Contains(e.VoucherId))
                .ToListAsync();
            context.LedgerEntries.RemoveRange(entries);
            context.Sales.RemoveRange(sales);

            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return (true, "Sales voucher deleted successfully");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return (false, $"Error deleting Sales: {ex.Message}");
        }
    }

    private static async Task<(bool valid, string message)> ValidateAsync(FruitAccountingContext context, SalesVoucherInput input, int? excludingInvNo)
    {
        if (input.Lines.Count == 0)
            return (false, "At least one buyer line is required");

        foreach (var line in input.Lines)
        {
            if (line.Quantity <= 0)
                return (false, "Every line needs a Qty greater than zero");
            if (line.Rate <= 0)
                return (false, "Every line needs a Rate greater than zero");
        }

        var lotItemExists = await context.PurchaseBillItems.AsNoTracking().AnyAsync(i => i.LotId == input.LotId);
        if (!lotItemExists)
            return (false, "Lot not found");

        var purchasedByLot = await GetPurchasedQtyByLotAsync(context, new List<long> { input.LotId });
        var purchasedQty = purchasedByLot.TryGetValue(input.LotId, out var pq) ? pq : 0;

        var soldByLot = await GetSoldQtyByLotAsync(context, new List<long> { input.LotId }, input.FinancialYearId, excludingInvNo);
        var soldSoFar = soldByLot.TryGetValue(input.LotId, out var s) ? s : 0;
        var newTotalQty = input.Lines.Sum(l => l.Quantity);

        if (soldSoFar + newTotalQty > purchasedQty)
        {
            var remaining = purchasedQty - soldSoFar;
            return (false, $"Total Qty ({newTotalQty}) exceeds this Lot's remaining balance ({remaining})");
        }

        return (true, "");
    }

    private static async Task<decimal> GetCompanyApmcPctAsync(FruitAccountingContext context, long companyId)
    {
        var company = await context.Companies.AsNoTracking().FirstOrDefaultAsync(c => c.CompanyId == companyId);
        return company?.ApmcPct ?? 0;
    }

    private static List<Sale> BuildSaleRows(SalesVoucherInput input, int invNo, decimal apmcPct, long itemId)
    {
        var sales = new List<Sale>();
        foreach (var line in input.Lines)
        {
            var labourAmount = Math.Round(line.LabourRate * line.Quantity, 2);
            // Rounded to whole rupees, not paise - matches real historical data (every APMC value
            // observed in the legacy `sales` table was a whole number) and the live UI preview.
            var apmcAmount = Math.Round(line.Amount * apmcPct / 100, 0);
            var netAmount = line.Amount + labourAmount + apmcAmount;

            sales.Add(new Sale
            {
                FinancialYearId = input.FinancialYearId,
                InvNo = invNo,
                VehNo = input.VehNo,
                SaleDate = input.SaleDate,
                LotId = input.LotId,
                BuyerId = line.BuyerId,
                ItemId = itemId,
                Quantity = line.Quantity,
                Weight = line.Weight,
                Rate = line.Rate,
                Amount = line.Amount,
                MarketFeePct = apmcPct,
                MarketFee = apmcAmount,
                LabourRate = line.LabourRate,
                Labour = labourAmount,
                NetAmount = netAmount,
                Remarks = line.Remarks,
                CreatedBy = input.CreatedBy,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }
        return sales;
    }

    private async Task<(bool success, string message)> BuildAndAddLedgerEntriesAsync(FruitAccountingContext context, List<Sale> sales, int invNo)
    {
        var salesAccountId = await GetConfiguredAccountIdAsync(context, SalesAccountKey);
        if (salesAccountId == null)
            return (false, "No Sales account is configured (system_parameters: SALES_ACCOUNT_ID)");

        var entries = new List<LedgerEntry>();

        foreach (var sale in sales)
        {
            // GL Routing: if buyer has Amanat Party, post to Amanat Party account; otherwise post to buyer
            var buyerPostingAccountId = (sale.Buyer?.AmanatPartyId ?? sale.BuyerId);

            // Audit trail: if posting to Amanat Party (not back to buyer), preserve buyer ID for traceability
            var originalAccountId = (sale.Buyer?.AmanatPartyId != null
                                     && sale.Buyer.AmanatPartyId != sale.BuyerId)
                ? sale.BuyerId
                : (long?)null;

            // What the buyer owes: goods cost plus the labour/APMC charges passed through to them.
            entries.Add(new LedgerEntry
            {
                FinancialYearId = sale.FinancialYearId,
                EntryDate = sale.SaleDate,
                AccountId = buyerPostingAccountId,
                OriginalAccountId = originalAccountId,
                Debit = sale.NetAmount,
                Credit = 0,
                VoucherId = sale.SaleId,
                VoucherType = VoucherType.SalesBill,
                Narration = $"Sales #{invNo}",
                ContraAccountId = salesAccountId.Value,
                Quantity = sale.Quantity,
                CreatedAt = DateTime.UtcNow
            });

            entries.Add(new LedgerEntry
            {
                FinancialYearId = sale.FinancialYearId,
                EntryDate = sale.SaleDate,
                AccountId = salesAccountId.Value,
                Debit = 0,
                Credit = sale.Amount,
                VoucherId = sale.SaleId,
                VoucherType = VoucherType.SalesBill,
                Narration = $"Sales #{invNo}",
                ContraAccountId = buyerPostingAccountId,
                CreatedAt = DateTime.UtcNow
            });

            if (sale.Labour != 0)
            {
                var hamaliAccountId = await GetConfiguredAccountIdAsync(context, HamaliPayableKey);
                if (hamaliAccountId == null)
                    return (false, "Labour amount computed, but no Labour/Hamali Payable account is configured (system_parameters: HAMALI_PAYABLE_ACCOUNT_ID)");

                entries.Add(new LedgerEntry
                {
                    FinancialYearId = sale.FinancialYearId,
                    EntryDate = sale.SaleDate,
                    AccountId = hamaliAccountId.Value,
                    Debit = 0,
                    Credit = sale.Labour ?? 0,
                    VoucherId = sale.SaleId,
                    VoucherType = VoucherType.SalesBill,
                    Narration = $"Labour on Sales #{invNo}",
                    ContraAccountId = buyerPostingAccountId,
                    OriginalAccountId = originalAccountId,
                    CreatedAt = DateTime.UtcNow
                });
            }

            if (sale.MarketFee != 0)
            {
                var apmcAccountId = await GetConfiguredAccountIdAsync(context, ApmcPayableKey);
                if (apmcAccountId == null)
                    return (false, "APMC amount computed, but no APMC Payable account is configured (system_parameters: APMC_PAYABLE_ACCOUNT_ID)");

                entries.Add(new LedgerEntry
                {
                    FinancialYearId = sale.FinancialYearId,
                    EntryDate = sale.SaleDate,
                    AccountId = apmcAccountId.Value,
                    Debit = 0,
                    Credit = sale.MarketFee ?? 0,
                    VoucherId = sale.SaleId,
                    VoucherType = VoucherType.SalesBill,
                    Narration = $"APMC on Sales #{invNo}",
                    ContraAccountId = buyerPostingAccountId,
                    OriginalAccountId = originalAccountId,
                    CreatedAt = DateTime.UtcNow
                });
            }
        }

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