using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class Company
{
    public long CompanyId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public string? City { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? PanNo { get; set; }

    public string? Gstin { get; set; }

    public string? ApmcLicenceNo { get; set; }

    public string? BankName { get; set; }

    public string? BankAccountNo { get; set; }

    public string? BankIfsc { get; set; }

    public decimal? ApmcPct { get; set; }

    public byte[]? Logo { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<AccountGroup> AccountGroups { get; set; } = new List<AccountGroup>();

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    public virtual ICollection<Country> Countries { get; set; } = new List<Country>();

    public virtual ICollection<Daybook> Daybooks { get; set; } = new List<Daybook>();

    public virtual ICollection<FinancialYear> FinancialYears { get; set; } = new List<FinancialYear>();

    public virtual ICollection<ItemCategory> ItemCategories { get; set; } = new List<ItemCategory>();

    public virtual ICollection<ItemCount> ItemCounts { get; set; } = new List<ItemCount>();

    public virtual ICollection<ItemGroup> ItemGroups { get; set; } = new List<ItemGroup>();

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual ICollection<Region> Regions { get; set; } = new List<Region>();

    public virtual ICollection<Salesman> Salesmen { get; set; } = new List<Salesman>();

    public virtual ICollection<Transporter> Transporters { get; set; } = new List<Transporter>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
