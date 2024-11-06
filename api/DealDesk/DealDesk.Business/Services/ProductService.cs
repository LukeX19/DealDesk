using AutoMapper;
using DealDesk.Business.Decorators;
using DealDesk.Business.Dtos;
using DealDesk.Business.Exceptions;
using DealDesk.Business.Interfaces;
using DealDesk.Business.Strategies;
using DealDesk.DataAccess.Entities;
using DealDesk.DataAccess.Interfaces;

namespace DealDesk.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, ICustomerRepository customerRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<long> Create(ProductRequest productDto, CancellationToken ct = default)
        {
            var product = _mapper.Map<Product>(productDto);
            var productId = await _productRepository.Create(product, ct);
            return productId;
        }

        public async Task<ICollection<ProductResponse>> GetAll(CancellationToken ct = default)
        {
            var products = await _productRepository.GetAll(ct);
            return _mapper.Map<ICollection<ProductResponse>>(products);
        }

        public async Task<ProductResponse> GetById(long productId, CancellationToken ct = default)
        {
            var product = await _productRepository.GetById(productId, ct);
            return _mapper.Map<ProductResponse>(product);
        }

        public async Task Update(long productId, ProductRequest updatedProductDto, CancellationToken ct = default)
        {
            var product = _mapper.Map<Product>(updatedProductDto);
            await _productRepository.Update(productId, product, ct);
        }

        public async Task Delete(long productId, CancellationToken ct = default)
        {
            await _productRepository.Delete(productId, ct);
        }

        public async Task<decimal> GetDiscountedPrice(long productId, int quantity, long customerId, CancellationToken ct = default)
        {
            if (quantity <= 0)
            {
                throw new InvalidQuantityException();
            }

            var product = await _productRepository.GetById(productId, ct);
            var customer = await _customerRepository.GetById(customerId, ct);

            // We start with the No Discount strategy first
            IDiscountStrategy discountStrategy = new NoDiscount();

            // Then, we check the agreed discount types for the customer
            foreach (var discountType in customer.DiscountStrategies)
            {
                discountStrategy = discountType.ToLowerInvariant() switch
                {
                    "volume" => new VolumeDiscountDecorator(discountStrategy),
                    "seasonal" => new SeasonalDiscountDecorator(discountStrategy),
                    _ => discountStrategy
                };
            }

            // Calculate total price before discount
            var totalPrice = product.StandardPrice * quantity;

            // Calculate the final discounted price
            return discountStrategy.ApplyDiscount(totalPrice);
        }
    }
}
