using DealDesk.Business.Dtos;

namespace DealDesk.Business.Interfaces
{
    public interface IProductService
    {
        Task<long> Create(ProductRequest productDto, CancellationToken ct = default);
        Task<ICollection<ProductResponse>> GetAll(CancellationToken ct = default);
        Task<ProductResponse> GetById(long productId, CancellationToken ct = default);
        Task Update(long productId, ProductRequest updatedProductDto, CancellationToken ct = default);
        Task Delete(long productId, CancellationToken ct = default);
        Task<decimal> GetDiscountedPrice(long productId, int quantity, long customerId, CancellationToken ct = default);
    }
}
