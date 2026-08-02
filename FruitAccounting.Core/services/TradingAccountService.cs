using FruitAccounting.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FruitAccounting.Core.services;

/// Trading A/c - the first half of the income statement. Everything classified as
/// TRADING EXPENSE sits on the left, everything TRADING INCOME on the right, and Gross Profit
/// (or Gross Loss) is whatever makes the two sides balance.
///
/// The two natures are what make this report possible: TRADING EXPENSE / TRADING INCOME are
/// distinct from plain EXPENSE / INCOME, so the split needs no name matching. Accounts may hang
/// off a main group directly or off one of its sub groups; either way the line is reported under
/// the main group, which is how the legacy screen presented it.
public class TradingAccountService
{
    private readonly IDbContextFactory<FruitAccountingContext> _contextFactory;

    public TradingAccountService(IDbContextFactory<FruitAccountingContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public const string TradingExpense = "TRADING EXPENSE";
    public const string TradingIncome = "TRADING INCOME";

    public class TradingLine
    {
        public string Text { get; set; } = "";
        public decimal? Amount { get; set; }
        public bool IsGroupHeader { get; set; }
        public bool IsSubTotal { get; set; }
        public bool IsGrossResult { get; set; }
        public bool IsGrandTotal { get; set; }
    }

    public class TradingResult
    {
        public List<TradingLine> Expense { get; set; } = new();
        public List<TradingLine> Income { get; set; } = new();

        public decimal TotalExpense { get; set; }
        public decimal TotalIncome { get; set; }

        /// Positive = Gross Profit, negative = Gross Loss.
        public decimal GrossProfit { get; set; }

        /// Both columns foot to this once the gross result is included.
        public decimal GrandTotal { get; set; }
    }

    private class AccountNet
    {
        public string Nature = "";
        public string MainGroup = "";
        public string AccountName = "";
        public decimal Debit;
        public decimal Credit;
    }

    public async Task<TradingResult> GetTradingAccountAsync(long companyId, long financialYearId,
        DateOnly fromDate, DateOnly toDate)
    {
        using var context = _contextFactory.CreateDbContext();

        // Period movements only. Trading A/c reports what happened between the two dates - unlike
        // a balance-sheet account, a trading account carries nothing forward into the period.
        var rows = await (
            from e in context.LedgerEntries
            join a in context.Accounts on e.AccountId equals a.AccountId
            join g in context.AccountGroups on a.AccountGroupId equals g.AccountGroupId
            where a.CompanyId == companyId
                  && e.FinancialYearId == financialYearId
                  && e.EntryDate >= fromDate && e.EntryDate <= toDate
                  && (g.Nature == TradingExpense || g.Nature == TradingIncome)
            select new
            {
                g.Nature,
                // an account on a sub group reports under its parent; one on a main group reports under itself
                MainGroup = g.Parent != null ? g.Parent.Name : g.Name,
                AccountName = a.Name,
                e.Debit,
                e.Credit
            }).ToListAsync();

        var nets = rows
            .GroupBy(r => new { r.Nature, r.MainGroup, r.AccountName })
            .Select(grp => new AccountNet
            {
                Nature = grp.Key.Nature,
                MainGroup = grp.Key.MainGroup,
                AccountName = grp.Key.AccountName,
                Debit = grp.Sum(x => x.Debit),
                Credit = grp.Sum(x => x.Credit)
            })
            .ToList();

        var result = new TradingResult();

        // Expense is debit-natured, income credit-natured. A negative figure is normal and
        // meaningful - Truck Fare recovers more on purchase bills than it pays out, so it shows
        // as a negative expense, exactly as the legacy screen did.
        result.Expense = BuildSide(nets.Where(n => n.Nature == TradingExpense), n => n.Debit - n.Credit,
            out var totalExpense);
        result.Income = BuildSide(nets.Where(n => n.Nature == TradingIncome), n => n.Credit - n.Debit,
            out var totalIncome);

        result.TotalExpense = totalExpense;
        result.TotalIncome = totalIncome;
        result.GrossProfit = totalIncome - totalExpense;
        result.GrandTotal = Math.Max(totalIncome, totalExpense);

        // The balancing figure goes on whichever side is short.
        var gross = new TradingLine
        {
            Text = result.GrossProfit >= 0 ? "GROSS PROFIT" : "GROSS LOSS",
            Amount = Math.Abs(result.GrossProfit),
            IsGrossResult = true
        };
        if (result.GrossProfit >= 0)
            result.Expense.Add(gross);
        else
            result.Income.Add(gross);

        result.Expense.Add(new TradingLine { Amount = result.GrandTotal, IsGrandTotal = true });
        result.Income.Add(new TradingLine { Amount = result.GrandTotal, IsGrandTotal = true });

        return result;
    }

    private static List<TradingLine> BuildSide(IEnumerable<AccountNet> accounts,
        Func<AccountNet, decimal> amount, out decimal total)
    {
        var lines = new List<TradingLine>();
        total = 0;

        foreach (var grp in accounts.GroupBy(a => a.MainGroup).OrderBy(g => g.Key))
        {
            lines.Add(new TradingLine { Text = grp.Key, IsGroupHeader = true });

            decimal subTotal = 0;
            foreach (var acc in grp.OrderBy(a => a.AccountName))
            {
                var value = amount(acc);
                lines.Add(new TradingLine { Text = acc.AccountName, Amount = value });
                subTotal += value;
            }

            lines.Add(new TradingLine { Amount = subTotal, IsSubTotal = true });
            total += subTotal;
        }

        return lines;
    }
}
