namespace ModernCrm.Api.DTOs
{
    public record AccountDto(
        int Id,
        string Company,
        string AccountNumber,
        int CustomerId,
        string CustomerNumber,
        string AccountType,
        decimal InterestRate,
        decimal OverdraftLimit,
        string SortCode,
        DateTime OpenDate,
        DateTime? LastStatementDate,
        DateTime? NextStatementDate,
        decimal AvailableBalance,
        decimal ActualBalance
    );

    public record CreateAccountDto(
        string Company,
        string AccountNumber,
        int CustomerId,
        string AccountType,
        decimal InterestRate,
        decimal OverdraftLimit,
        string SortCode,
        DateTime OpenDate,
        decimal InitialDeposit
    );

    public record UpdateAccountDto(
        string Company,
        string AccountType,
        decimal InterestRate,
        decimal OverdraftLimit,
        string SortCode,
        DateTime? LastStatementDate,
        DateTime? NextStatementDate
    );
}