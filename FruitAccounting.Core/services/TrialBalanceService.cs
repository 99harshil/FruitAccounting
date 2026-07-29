using FruitAccounting.Data.Context;
using FruitAccounting.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FruitAccounting.Core.services;

public class TrialBalanceService
{
    private readonly IDbContextFactory<FruitAccountingContext> _contextFactory;
    private readonly LedgerService _ledgerService;

    public TrialBalanceService(IDbContextFactory<FruitAccountingContext> contextFactory, LedgerService ledgerService)
    {
        _contextFactory = contextFactory;
        _ledgerService = ledgerService;
    }

    public class TrialBalanceRow
    {
        public long AccountId { get; set; }
        public string AccountCode { get; set; } = "";
        public string AccountName { get; set; } = "";
        public long AccountGroupId { get; set; }
        public string GroupName { get; set; } = "";

        public decimal OpeningCr { get; set; }
        public decimal OpeningDr { get; set; }
        public decimal TransactionCr { get; set; }
        public decimal TransactionDr { get; set; }
        public decimal ClosingCr { get; set; }
        public decimal ClosingDr { get; set; }

        public bool IsGroupHeader { get; set; }
        public bool IsSubTotal { get; set; }
        public bool IsGrandTotal { get; set; }
    }

    public async Task<List<TrialBalanceRow>> GetTrialBalanceAsync(long companyId, long financialYearId, DateOnly fromDate, DateOnly toDate)
    {
        using var context = _contextFactory.CreateDbContext();

        var accounts = await context.Accounts
            .AsNoTracking()
            .Include(a => a.AccountGroup)
            .Where(a => a.CompanyId == companyId && !a.IsBlocked && a.AccountGroupId > 0)
            .OrderBy(a => a.AccountGroup!.Name)
            .ThenBy(a => a.Code)
            .ToListAsync();

        var rows = new List<TrialBalanceRow>();
        var groupedAccounts = accounts.GroupBy(a => a.AccountGroupId).ToList();

        decimal grandTotalCr = 0, grandTotalDr = 0;
        decimal grandTotalTrCr = 0, grandTotalTrDr = 0;
        decimal grandTotalClCr = 0, grandTotalClDr = 0;

        foreach (var group in groupedAccounts)
        {
            var groupName = group.FirstOrDefault()?.AccountGroup?.Name ?? "Unknown";
            var groupId = group.Key;

            rows.Add(new TrialBalanceRow
            {
                IsGroupHeader = true,
                GroupName = groupName,
                AccountGroupId = groupId
            });

            decimal groupTotalCr = 0, groupTotalDr = 0;
            decimal groupTotalTrCr = 0, groupTotalTrDr = 0;
            decimal groupTotalClCr = 0, groupTotalClDr = 0;

            foreach (var account in group.OrderBy(a => a.Code))
            {
                var ledger = await _ledgerService.GetLedgerAsync(account.AccountId, financialYearId, fromDate, toDate);

                var openingCr = ledger.OpeningBalance < 0 ? Math.Abs(ledger.OpeningBalance) : 0;
                var openingDr = ledger.OpeningBalance > 0 ? ledger.OpeningBalance : 0;

                var closingCr = ledger.ClosingBalance < 0 ? Math.Abs(ledger.ClosingBalance) : 0;
                var closingDr = ledger.ClosingBalance > 0 ? ledger.ClosingBalance : 0;

                var trCr = ledger.TotalCredit;
                var trDr = ledger.TotalDebit;

                rows.Add(new TrialBalanceRow
                {
                    AccountId = account.AccountId,
                    AccountCode = account.Code,
                    AccountName = account.Name,
                    AccountGroupId = account.AccountGroupId,
                    GroupName = groupName,
                    OpeningCr = openingCr,
                    OpeningDr = openingDr,
                    TransactionCr = trCr,
                    TransactionDr = trDr,
                    ClosingCr = closingCr,
                    ClosingDr = closingDr
                });

                groupTotalCr += openingCr;
                groupTotalDr += openingDr;
                groupTotalTrCr += trCr;
                groupTotalTrDr += trDr;
                groupTotalClCr += closingCr;
                groupTotalClDr += closingDr;
            }

            rows.Add(new TrialBalanceRow
            {
                IsSubTotal = true,
                GroupName = $"SUB TOTAL: {groupName}",
                OpeningCr = groupTotalCr,
                OpeningDr = groupTotalDr,
                TransactionCr = groupTotalTrCr,
                TransactionDr = groupTotalTrDr,
                ClosingCr = groupTotalClCr,
                ClosingDr = groupTotalClDr
            });

            grandTotalCr += groupTotalCr;
            grandTotalDr += groupTotalDr;
            grandTotalTrCr += groupTotalTrCr;
            grandTotalTrDr += groupTotalTrDr;
            grandTotalClCr += groupTotalClCr;
            grandTotalClDr += groupTotalClDr;
        }

        rows.Add(new TrialBalanceRow
        {
            IsGrandTotal = true,
            GroupName = "GRAND TOTAL :-",
            OpeningCr = grandTotalCr,
            OpeningDr = grandTotalDr,
            TransactionCr = grandTotalTrCr,
            TransactionDr = grandTotalTrDr,
            ClosingCr = grandTotalClCr,
            ClosingDr = grandTotalClDr
        });

        return rows;
    }
}
