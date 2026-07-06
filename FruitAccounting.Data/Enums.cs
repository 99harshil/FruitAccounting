namespace FruitAccounting.Data.Enums;

public enum PurchaseMode { WithCommission, Trading, WithoutCommission }

public enum VoucherType
{
    PurchaseBill, SalesBill, Receipt, Payment, Journal, BankEntry,
    Crate, ColdStorage, DesavarPurchase, DesavarSale,
    ImportPurchase, ImportSale, OpeningBalance
}

public enum PaymentMode { Cash, Bank, Cheque }
public enum DrCr { Debit, Credit }
public enum CrateTxnType { Issue, Return, AmountConversion }
public enum ColdTxnType { Inward, Outward }
public enum LotOpType { Split, Merge, Transfer }
public enum UserRole { Admin, Operator, Readonly }
public enum DispatchStatus { Queued, Sent, Delivered, Read, Failed }