using DealDesk.Business.Dtos;

namespace DealDesk.Business.Interfaces
{
    public interface ICustomerService
    {
        Task<long> Create(CustomerRequest customerDto, CancellationToken ct = default);
        Task<ICollection<CustomerResponse>> GetAll(CancellationToken ct = default);
        Task<CustomerResponse> GetById(long customerId, CancellationToken ct = default);
        Task Update(long customerId, CustomerRequest updatedCustomerDto, CancellationToken ct = default);
        Task Delete(long customerId, CancellationToken ct = default);
    }
}
