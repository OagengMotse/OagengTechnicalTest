using CsvHelper.Configuration.Attributes;

namespace OagengTechnicalTest.Data.Models
{
    public class Transaction
    {   [Name("Payment ID")]
        public int PaymentId { get; set; }
        [Name("Account Holder")]
        public string AccountHolder { get; set; }
        [Name("Branch Code")]
        public int BranchCode { get; set; }
        [Name("Account Number")]
        public Int64 AccountNumber { get; set; }
        [Name("Account Type")]
        public int AccountType { get; set; }
        [Name("Transaction Date")]
        public string TransactionDate { get; set; }
        [Name("Amount")]
        public double Amount { get; set; }
        [Name("Status")]
        public string Status { get; set; }
        [Name("Effective Status Date")]
        public string EffectiveStatusDate { get; set; }
    }
}
