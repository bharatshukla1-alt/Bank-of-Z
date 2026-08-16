using Microsoft.EntityFrameworkCore;
using ModernCrm.Api.Data;
using ModernCrm.Api.DTOs;
using ModernCrm.Api.Models;

namespace ModernCrm.Api.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly CrmDbContext _context;

        public TransactionService(CrmDbContext context)
        {
            _context = context;
        }

        public async Task<TransactionResultDto> DepositOrWithdrawAsync(DepositWithdrawRequestDto request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == request.AccountNumber);
                if (account == null)
                {
                    return new TransactionResultDto(false, "Account not found", 0, 0, DateTime.UtcNow);
                }

                if (request.Sign == "-")
                {
                    if (account.AvailableBalance + account.OverdraftLimit < request.Amount)
                    {
                        return new TransactionResultDto(false, "Insufficient funds including overdraft limit", account.AvailableBalance, account.ActualBalance, DateTime.UtcNow);
                    }
                    account.AvailableBalance -= request.Amount;
                    account.ActualBalance -= request.Amount;
                }
                else
                {
                    account.AvailableBalance += request.Amount;
                    account.ActualBalance += request.Amount;
                }

                var record = new Transaction
                {
                    Company = request.Company,
                    FromAccountNumber = request.AccountNumber,
                    FromSortCode = request.SortCode,
                    Amount = request.Amount,
                    Sign = request.Sign,
                    TransactionType = request.Sign == "-" ? "Withdrawal" : "Deposit",
                    TransactionDate = DateTime.UtcNow,
                    Message = request.Sign == "-" ? "Withdrawal processed" : "Deposit processed"
                };

                _context.Transactions.Add(record);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new TransactionResultDto(true, "Transaction successful", account.AvailableBalance, account.ActualBalance, record.TransactionDate);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new TransactionResultDto(false, $"Error processing transaction: {ex.Message}", 0, 0, DateTime.UtcNow);
            }
        }

        public async Task<TransactionResultDto> TransferFundsAsync(TransferRequestDto request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var fromAcc = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == request.FromAccountNumber);
                var toAcc = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == request.ToAccountNumber);

                if (fromAcc == null || toAcc == null)
                {
                    return new TransactionResultDto(false, "Source or target account not found", 0, 0, DateTime.UtcNow);
                }

                if (fromAcc.AvailableBalance + fromAcc.OverdraftLimit < request.Amount)
                {
                    return new TransactionResultDto(false, "Insufficient funds for transfer", fromAcc.AvailableBalance, fromAcc.ActualBalance, DateTime.UtcNow);
                }

                fromAcc.AvailableBalance -= request.Amount;
                fromAcc.ActualBalance -= request.Amount;

                toAcc.AvailableBalance += request.Amount;
                toAcc.ActualBalance += request.Amount;

                var txn = new Transaction
                {
                    Company = request.Company,
                    FromAccountNumber = request.FromAccountNumber,
                    FromSortCode = request.FromSortCode,
                    ToAccountNumber = request.ToAccountNumber,
                    ToSortCode = request.ToSortCode,
                    Amount = request.Amount,
                    Sign = "-",
                    TransactionType = "Transfer",
                    TransactionDate = DateTime.UtcNow,
                    Message = $"Transferred {request.Amount:C} to {request.ToAccountNumber}"
                };

                _context.Transactions.Add(txn);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new TransactionResultDto(true, "Transfer completed successfully", fromAcc.AvailableBalance, fromAcc.ActualBalance, txn.TransactionDate);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new TransactionResultDto(false, $"Transfer failed: {ex.Message}", 0, 0, DateTime.UtcNow);
            }
        }
    }
}