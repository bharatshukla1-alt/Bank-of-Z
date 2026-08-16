using Microsoft.AspNetCore.Mvc;
using ModernCrm.Api.DTOs;
using ModernCrm.Api.Services;

namespace ModernCrm.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("deposit-withdraw")]
        public async Task<ActionResult<TransactionResultDto>> DepositOrWithdraw([FromBody] DepositWithdrawRequestDto request)
        {
            var result = await _transactionService.DepositOrWithdrawAsync(request);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("transfer")]
        public async Task<ActionResult<TransactionResultDto>> Transfer([FromBody] TransferRequestDto request)
        {
            var result = await _transactionService.TransferFundsAsync(request);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
    }
}