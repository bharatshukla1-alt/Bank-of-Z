namespace ModernCrm.Api.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Company { get; set; } = string.Empty;
        public string CustomerNumber { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleInitials { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string AddressLine1 { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string SortCode { get; set; } = string.Empty;
        public int CreditScore { get; set; }
        public DateTime? ScoreDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<Account> Accounts { get; set; } = new();
    }
}