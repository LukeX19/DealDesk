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

        public long Create(CustomerRequest customerDto)
        {
            Customer customer = _mapper.Map<Customer>(customerDto);
            var customerId = _customerRepository.Create(customer);
            return customerId;
        }

        public ICollection<CustomerResponse> GetAll()
        {
            ICollection<Customer> customers = _customerRepository.GetAll();
            return _mapper.Map<ICollection<CustomerResponse>>(customers);
        }

        public CustomerResponse GetById(long customerId)
        {
            Customer customer = _customerRepository.GetById(customerId);
            return _mapper.Map<CustomerResponse>(customer);
        }

        public void Update(long customerId, CustomerRequest updatedCustomerDto)
        {
            Customer customer = _mapper.Map<Customer>(updatedCustomerDto);
            _customerRepository.Update(customerId, customer);
        }

        public void Delete(long customerId)
        {
            _customerRepository.Delete(customerId);
        }
    }
}
