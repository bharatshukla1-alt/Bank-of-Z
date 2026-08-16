namespace ModernCrm.Api.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Company { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public string AccountType { get; set; } = string.Empty;
        public decimal InterestRate { get; set; }
        public decimal OverdraftLimit { get; set; }
        public string SortCode { get; set; } = string.Empty;
        public DateTime OpenDate { get; set; } = DateTime.UtcNow;
        public DateTime? LastStatementDate { get; set; }
        public DateTime? NextStatementDate { get; set; }
        public decimal AvailableBalance { get; set; }
        public decimal ActualBalance { get; set; }
    }
}