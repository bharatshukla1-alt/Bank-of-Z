namespace ModernCrm.Api.DTOs
{
    public record CustomerDto(
        int Id,
        string Company,
        string CustomerNumber,
        string Title,
        string FirstName,
        string MiddleInitials,
        string LastName,
        string AddressLine1,
        string AddressLine2,
        string City,
        string PostCode,
        string Country,
        DateTime? DateOfBirth,
        string SortCode,
        int CreditScore,
        DateTime? ScoreDate
    );

    public record CreateCustomerDto(
        string Company,
        string CustomerNumber,
        string Title,
        string FirstName,
        string MiddleInitials,
        string LastName,
        string AddressLine1,
        string AddressLine2,
        string City,
        string PostCode,
        string Country,
        DateTime? DateOfBirth,
        string SortCode,
        int CreditScore
    );

    public record UpdateCustomerDto(
        string Company,
        string Title,
        string FirstName,
        string MiddleInitials,
        string LastName,
        string AddressLine1,
        string AddressLine2,
        string City,
        string PostCode,
        string Country,
        DateTime? DateOfBirth,
        string SortCode,
        int CreditScore
    );
}