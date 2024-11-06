using DealDesk.Business.Dtos;

namespace DealDesk.Business.Interfaces
{
    public interface ICustomerService
    {
        long Create(CustomerRequest customerDto);
        ICollection<CustomerResponse> GetAll();
        CustomerResponse GetById(long customerId);
        void Update(long customerId, CustomerRequest updatedCustomerDto);
        void Delete(long customerId);
    }
}
