using DealDesk.Business.Dtos;
using DealDesk.Business.Interfaces;
using DealDesk.Business.Strategies;
using Microsoft.AspNetCore.Mvc;

namespace DealDesk.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productsService)
        {
            _productService = productsService;
        }

        [HttpPost]
        public IActionResult Create(ProductRequest productDto)
        {
            var createdProductId = _productService.Create(productDto);
            var createdProduct = _productService.GetById(createdProductId);
            return CreatedAtAction(nameof(GetById), new { productId = createdProductId }, createdProduct);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productService.GetAll();
            return Ok(products);
        }

        [HttpGet("{productId}")]
        public IActionResult GetById(long productId)
        {
            var product = _productService.GetById(productId);
            return Ok(product);
        }

        [HttpPut("{productId}")]
        public IActionResult Update(long productId, ProductRequest updatedProductDto)
        {
            _productService.Update(productId, updatedProductDto);
            return NoContent();
        }

        [HttpDelete("{productId}")]
        public IActionResult Delete(long productId)
        {
            _productService.Delete(productId);
            return NoContent();
        }

        [HttpPost("{productId}/discount")]
        public IActionResult ApplyDiscount(long productId, DiscountRequest discountRequest)
        {
            IDiscountStrategy discountStrategy = discountRequest.DiscountType switch
            {
                "Volume" => new VolumeDiscount(),
                "Seasonal" => new SeasonalDiscount(),
                _ => new NoDiscount()
            };

            var discountedPrice = _productService.GetDiscountedPrice(productId, discountStrategy);

            return Ok(new { ProductId = productId, DiscountedPrice = discountedPrice });
        }
    }
}
