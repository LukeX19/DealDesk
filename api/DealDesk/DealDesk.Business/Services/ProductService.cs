using AutoMapper;
using DealDesk.Business.Decorators;
using DealDesk.Business.Dtos;
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

        public decimal GetDiscountedPrice(long productId, long customerId, int quantity)
        {
            var product = _productRepository.GetById(productId);
            var customer = _customerRepository.GetById(customerId);

            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
            }

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
