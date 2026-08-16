using Microsoft.AspNetCore.Mvc;
using ModernCrm.Api.DTOs;
using ModernCrm.Api.Services;

namespace ModernCrm.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("customer/{custNo}")]
        public async Task<ActionResult<IEnumerable<AccountDto>>> GetByCustomer(string custNo)
        {
            var accounts = await _accountService.GetAccountsByCustomerNumberAsync(custNo);
            return Ok(accounts);
        }

        [HttpGet("{accNo}")]
        public async Task<ActionResult<AccountDto>> GetByAccountNo(string accNo)
        {
            var account = await _accountService.GetAccountByNumberAsync(accNo);
            if (account == null) return NotFound(new { Message = "Account not found" });
            return Ok(account);
        }

        [HttpPost]
        public async Task<ActionResult<AccountDto>> Create([FromBody] CreateAccountDto dto)
        {
            var created = await _accountService.CreateAccountAsync(dto);
            return CreatedAtAction(nameof(GetByAccountNo), new { accNo = created.AccountNumber }, created);
        }

        [HttpPut("{accNo}")]
        public async Task<ActionResult<AccountDto>> Update(string accNo, [FromBody] UpdateAccountDto dto)
        {
            var updated = await _accountService.UpdateAccountAsync(accNo, dto);
            if (updated == null) return NotFound(new { Message = "Account not found" });
            return Ok(updated);
        }

        [HttpDelete("{accNo}")]
        public async Task<IActionResult> Delete(string accNo)
        {
            var success = await _accountService.DeleteAccountAsync(accNo);
            if (!success) return NotFound(new { Message = "Account not found" });
            return NoContent();
        }
    }
}