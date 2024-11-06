using DealDesk.DataAccess.Entities;
using DealDesk.DataAccess.Interfaces;

namespace DealDesk.DataAccess.Repositories
{
    public class CustomerRepository : BaseRepositorySQLite<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context) {  }
    }
}
