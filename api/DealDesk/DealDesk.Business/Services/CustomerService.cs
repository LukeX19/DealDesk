using AutoMapper;
using DealDesk.Business.Dtos;
using DealDesk.Business.Interfaces;
using DealDesk.DataAccess.Entities;
using DealDesk.DataAccess.Interfaces;

namespace DealDesk.Business.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<long> Create(CustomerRequest customerDto, CancellationToken ct = default)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            var customerId = await _customerRepository.Create(customer, ct);
            return customerId;
        }

        public async Task<ICollection<CustomerResponse>> GetAll(CancellationToken ct = default)
        {
            var customers = await _customerRepository.GetAll(ct);
            return _mapper.Map<ICollection<CustomerResponse>>(customers);
        }

        public async Task<CustomerResponse> GetById(long customerId, CancellationToken ct = default)
        {
            var customer = await _customerRepository.GetById(customerId, ct);
            return _mapper.Map<CustomerResponse>(customer);
        }

        public async Task Update(long customerId, CustomerRequest updatedCustomerDto, CancellationToken ct = default)
        {
            var customer = _mapper.Map<Customer>(updatedCustomerDto);
            await _customerRepository.Update(customerId, customer, ct);
        }

        public async Task Delete(long customerId, CancellationToken ct = default)
        {
            await _customerRepository.Delete(customerId, ct);
        }
    }
}
