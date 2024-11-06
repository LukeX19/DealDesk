using DealDesk.Business.Dtos;
using DealDesk.Business.Interfaces;
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

        [HttpGet("discount")]
        public IActionResult GetDiscountedPrice([FromQuery] ProductDiscountRequest request)
        {
            // Calculate the discounted price using the ProductService
            var discountedTotalPrice = _productService.GetDiscountedPrice(request.ProductId, request.CustomerId, request.Quantity);

            // Return the discounted price
            var response = new ProductDiscountResponse
            {
                ProductId = request.ProductId,
                CustomerId = request.CustomerId,
                Quantity = request.Quantity,
                DiscountedTotalPrice = discountedTotalPrice
            };
            return Ok(response);
        }
    }
}
