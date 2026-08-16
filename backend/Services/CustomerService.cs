using Microsoft.EntityFrameworkCore;
using ModernCrm.Api.Data;
using ModernCrm.Api.DTOs;
using ModernCrm.Api.Models;

namespace ModernCrm.Api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CrmDbContext _context;

        public CustomerService(CrmDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            return await _context.Customers
                .Select(c => MapToDto(c))
                .ToListAsync();
        }

        public async Task<CustomerDto?> GetCustomerByNumberAsync(string customerNumber)
        {
            var cust = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerNumber == customerNumber);
            return cust != null ? MapToDto(cust) : null;
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(int id)
        {
            var cust = await _context.Customers.FindAsync(id);
            return cust != null ? MapToDto(cust) : null;
        }

        public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto dto)
        {
            var customer = new Customer
            {
                Company = dto.Company,
                CustomerNumber = dto.CustomerNumber,
                Title = dto.Title,
                FirstName = dto.FirstName,
                MiddleInitials = dto.MiddleInitials,
                LastName = dto.LastName,
                AddressLine1 = dto.AddressLine1,
                AddressLine2 = dto.AddressLine2,
                City = dto.City,
                PostCode = dto.PostCode,
                Country = dto.Country,
                DateOfBirth = dto.DateOfBirth,
                SortCode = dto.SortCode,
                CreditScore = dto.CreditScore,
                ScoreDate = DateTime.UtcNow
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return MapToDto(customer);
        }

        public async Task<CustomerDto?> UpdateCustomerAsync(string customerNumber, UpdateCustomerDto dto)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerNumber == customerNumber);
            if (customer == null) return null;

            customer.Company = dto.Company;
            customer.Title = dto.Title;
            customer.FirstName = dto.FirstName;
            customer.MiddleInitials = dto.MiddleInitials;
            customer.LastName = dto.LastName;
            customer.AddressLine1 = dto.AddressLine1;
            customer.AddressLine2 = dto.AddressLine2;
            customer.City = dto.City;
            customer.PostCode = dto.PostCode;
            customer.Country = dto.Country;
            customer.DateOfBirth = dto.DateOfBirth;
            customer.SortCode = dto.SortCode;
            customer.CreditScore = dto.CreditScore;
            customer.ScoreDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return MapToDto(customer);
        }

        public async Task<bool> DeleteCustomerAsync(string customerNumber)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerNumber == customerNumber);
            if (customer == null) return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        private static CustomerDto MapToDto(Customer c) => new(
            c.Id,
            c.Company,
            c.CustomerNumber,
            c.Title,
            c.FirstName,
            c.MiddleInitials,
            c.LastName,
            c.AddressLine1,
            c.AddressLine2,
            c.City,
            c.PostCode,
            c.Country,
            c.DateOfBirth,
            c.SortCode,
            c.CreditScore,
            c.ScoreDate
        );
    }
}