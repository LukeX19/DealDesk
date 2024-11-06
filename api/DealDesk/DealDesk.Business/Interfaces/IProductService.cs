using DealDesk.Business.Dtos;

namespace DealDesk.Business.Interfaces
{
    public interface IProductService
    {
        long Create(ProductRequest productDto);
        ICollection<ProductResponse> GetAll();
        ProductResponse GetById(long productId);
        void Update(long productId, ProductRequest updatedProductDto);
        void Delete(long productId);
        decimal GetDiscountedPrice(long productId, long customerId, int quantity);
    }
}
