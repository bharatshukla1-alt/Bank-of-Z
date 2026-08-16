using Microsoft.AspNetCore.Mvc;
using ModernCrm.Api.DTOs;
using ModernCrm.Api.Services;

namespace ModernCrm.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("{custNo}")]
        public async Task<ActionResult<CustomerDto>> GetByCustomerNumber(string custNo)
        {
            var customer = await _customerService.GetCustomerByNumberAsync(custNo);
            if (customer == null) return NotFound(new { Message = "Customer not found" });
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> Create([FromBody] CreateCustomerDto dto)
        {
            var created = await _customerService.CreateCustomerAsync(dto);
            return CreatedAtAction(nameof(GetByCustomerNumber), new { custNo = created.CustomerNumber }, created);
        }

        [HttpPut("{custNo}")]
        public async Task<ActionResult<CustomerDto>> Update(string custNo, [FromBody] UpdateCustomerDto dto)
        {
            var updated = await _customerService.UpdateCustomerAsync(custNo, dto);
            if (updated == null) return NotFound(new { Message = "Customer not found" });
            return Ok(updated);
        }

        [HttpDelete("{custNo}")]
        public async Task<IActionResult> Delete(string custNo)
        {
            var success = await _customerService.DeleteCustomerAsync(custNo);
            if (!success) return NotFound(new { Message = "Customer not found" });
            return NoContent();
        }
    }
}