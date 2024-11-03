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
        public IActionResult Create(CustomerRequest customerDto)
        {
            var createdCustomerId = _customerService.Create(customerDto);
            var createdCustomer = _customerService.GetById(createdCustomerId);
            return CreatedAtAction(nameof(GetById), new { customerId = createdCustomerId }, createdCustomer);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var customers = _customerService.GetAll();
            return Ok(customers);
        }

        [HttpGet("{customerId}")]
        public IActionResult GetById(long customerId)
        {
            var customer = _customerService.GetById(customerId);
            return Ok(customer);
        }

        [HttpPut("{customerId}")]
        public IActionResult Update(long customerId, CustomerRequest updatedCustomerDto)
        {
            _customerService.Update(customerId, updatedCustomerDto);
            return NoContent();
        }

        [HttpDelete("{customerId}")]
        public IActionResult Delete(long customerId)
        {
            _customerService.Delete(customerId);
            return NoContent();
        }
    }
}
