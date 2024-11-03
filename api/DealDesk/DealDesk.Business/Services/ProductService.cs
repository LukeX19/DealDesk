using AutoMapper;
using DealDesk.Business.Dtos;
using DealDesk.Business.Interfaces;
using DealDesk.DataAccess.Entities;
using DealDesk.DataAccess.Interfaces;

namespace DealDesk.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public long Create(ProductRequest productDto)
        {
            Product product = _mapper.Map<Product>(productDto);
            var productId = _productRepository.Create(product);
            return productId;
        }

        public ICollection<ProductResponse> GetAll()
        {
            ICollection<Product> products = _productRepository.GetAll();
            return _mapper.Map<ICollection<ProductResponse>>(products);
        }

        public ProductResponse GetById(long productId)
        {
            Product product = _productRepository.GetById(productId);
            return _mapper.Map<ProductResponse>(product);
        }

        public void Update(long productId, ProductRequest updatedProductDto)
        {
            Product product = _mapper.Map<Product>(updatedProductDto);
            _productRepository.Update(productId, product);
        }

        public void Delete(long productId)
        {
            _productRepository.Delete(productId);
        }

        public decimal GetDiscountedPrice(long productId, IDiscountStrategy discountStrategy)
        {
            var product = _productRepository.GetById(productId);

            // Use the discount strategy to calculate the discounted price
            var priceCalculator = new PriceCalculator(discountStrategy);
            return priceCalculator.CalculatePrice(product);
        }
    }
}
