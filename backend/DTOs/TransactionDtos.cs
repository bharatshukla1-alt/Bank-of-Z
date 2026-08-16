namespace ModernCrm.Api.DTOs
{
    public record TransferRequestDto(
        string Company,
        string FromAccountNumber,
        string FromSortCode,
        string ToAccountNumber,
        string ToSortCode,
        decimal Amount
    );

    public record DepositWithdrawRequestDto(
        string Company,
        string AccountNumber,
        string SortCode,
        decimal Amount,
        string Sign // "+" for deposit, "-" for withdrawal
    );

    public record TransactionResultDto(
        bool Success,
        string Message,
        decimal UpdatedAvailableBalance,
        decimal UpdatedActualBalance,
        DateTime TransactionTime
    );
}