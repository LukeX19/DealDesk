using DealDesk.DataAccess.Entities;
using DealDesk.DataAccess.Interfaces;

namespace DealDesk.DataAccess.Repositories
{
    public class CustomerRepository : BaseRepositoryInMemory<Customer>, ICustomerRepository
    {
    }
}
