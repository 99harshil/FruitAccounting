namespace FruitAccounting.UI;

using Microsoft.EntityFrameworkCore;
using FruitAccounting.Data.Context;

public partial class Form1 : Form
{
    private readonly IDbContextFactory<FruitAccountingContext> _contextFactory;

    public Form1(IDbContextFactory<FruitAccountingContext> contextFactory)
    {
        InitializeComponent();
        _contextFactory = contextFactory;
    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }

    private void label1_Click(object sender, EventArgs e)
    {

    }

    private void button1_Click(object sender, EventArgs e)
    {
        try
        {
            using var context = _contextFactory.CreateDbContext();

            var bill = context.PurchaseBills
                .Include(b => b.Supplier)
                .First(b => b.BillNo == 1);

            label1.Text = $"✓ Connected!  Mode: {bill.Mode}  Net: {bill.NetAmount:N2}  ({bill.Supplier.Name})";
            label1.ForeColor = System.Drawing.Color.Green;
        }
        catch (Exception ex)
        {
            label1.Text = "✗ Failed: " + ex.Message;
            label1.ForeColor = System.Drawing.Color.Red;
        }
    }
}
