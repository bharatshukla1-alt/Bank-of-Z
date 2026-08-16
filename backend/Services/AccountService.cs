using Microsoft.EntityFrameworkCore;
using ModernCrm.Api.Data;
using ModernCrm.Api.DTOs;
using ModernCrm.Api.Models;

namespace ModernCrm.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly CrmDbContext _context;

        public AccountService(CrmDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AccountDto>> GetAccountsByCustomerNumberAsync(string customerNumber)
        {
            return await _context.Accounts
                .Include(a => a.Customer)
                .Where(a => a.Customer != null && a.Customer.CustomerNumber == customerNumber)
                .Select(a => MapToDto(a))
                .ToListAsync();
        }

        public async Task<AccountDto?> GetAccountByNumberAsync(string accountNumber)
        {
            var account = await _context.Accounts
                .Include(a => a.Customer)
                .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
            return account != null ? MapToDto(account) : null;
        }

        public async Task<AccountDto> CreateAccountAsync(CreateAccountDto dto)
        {
            var account = new Account
            {
                Company = dto.Company,
                AccountNumber = dto.AccountNumber,
                CustomerId = dto.CustomerId,
                AccountType = dto.AccountType,
                InterestRate = dto.InterestRate,
                OverdraftLimit = dto.OverdraftLimit,
                SortCode = dto.SortCode,
                OpenDate = dto.OpenDate,
                AvailableBalance = dto.InitialDeposit,
                ActualBalance = dto.InitialDeposit
            };

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            var reloaded = await _context.Accounts.Include(a => a.Customer).FirstAsync(a => a.Id == account.Id);
            return MapToDto(reloaded);
        }

        public async Task<AccountDto?> UpdateAccountAsync(string accountNumber, UpdateAccountDto dto)
        {
            var account = await _context.Accounts.Include(a => a.Customer).FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
            if (account == null) return null;

            account.Company = dto.Company;
            account.AccountType = dto.AccountType;
            account.InterestRate = dto.InterestRate;
            account.OverdraftLimit = dto.OverdraftLimit;
            account.SortCode = dto.SortCode;
            account.LastStatementDate = dto.LastStatementDate;
            account.NextStatementDate = dto.NextStatementDate;

            await _context.SaveChangesAsync();
            return MapToDto(account);
        }

        public async Task<bool> DeleteAccountAsync(string accountNumber)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
            if (account == null) return false;

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return true;
        }

        private static AccountDto MapToDto(Account a) => new(
            a.Id,
            a.Company,
            a.AccountNumber,
            a.CustomerId,
            a.Customer?.CustomerNumber ?? string.Empty,
            a.AccountType,
            a.InterestRate,
            a.OverdraftLimit,
            a.SortCode,
            a.OpenDate,
            a.LastStatementDate,
            a.NextStatementDate,
            a.AvailableBalance,
            a.ActualBalance
        );
    }
}