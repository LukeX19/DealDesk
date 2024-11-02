using DealDesk.DataAccess.Entities;
using DealDesk.DataAccess.Interfaces;

namespace DealDesk.DataAccess.Repositories
{
    public class ProductRepository : BaseRepositoryInMemory<Product>, IProductRepository
    {
    }
}
