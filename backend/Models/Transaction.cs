namespace ModernCrm.Api.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Company { get; set; } = string.Empty;
        public string FromAccountNumber { get; set; } = string.Empty;
        public string ToAccountNumber { get; set; } = string.Empty;
        public string FromSortCode { get; set; } = string.Empty;
        public string ToSortCode { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Sign { get; set; } = "+";
        public string TransactionType { get; set; } = string.Empty; // Deposit, Withdrawal, Transfer
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public string Message { get; set; } = string.Empty;
    }
}