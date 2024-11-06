using DealDesk.DataAccess.Entities;
using DealDesk.DataAccess.Interfaces;

namespace DealDesk.DataAccess.Repositories
{
    public class ProductRepository : BaseRepositorySQLite<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }
    }
}
