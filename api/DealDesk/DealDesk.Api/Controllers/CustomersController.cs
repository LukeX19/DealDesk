using DealDesk.Business.Dtos;
using DealDesk.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DealDesk.Api.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerRequest customerDto, CancellationToken ct = default)
        {
            var createdCustomerId = await _customerService.Create(customerDto, ct);
            var createdCustomer = await _customerService.GetById(createdCustomerId, ct);
            return CreatedAtAction(nameof(GetById), new { customerId = createdCustomerId }, createdCustomer);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct = default)
        {
            var customers = await _customerService.GetAll(ct);
            return Ok(customers);
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetById(long customerId, CancellationToken ct = default)
        {
            var customer = await _customerService.GetById(customerId, ct);
            return Ok(customer);
        }

        [HttpPut("{customerId}")]
        public async Task<IActionResult> Update(long customerId, CustomerRequest updatedCustomerDto, CancellationToken ct = default)
        {
            await _customerService.Update(customerId, updatedCustomerDto, ct);
            return NoContent();
        }

        [HttpDelete("{customerId}")]
        public async Task<IActionResult> Delete(long customerId, CancellationToken ct = default)
        {
            await _customerService.Delete(customerId, ct);
            return NoContent();
        }
    }
}
