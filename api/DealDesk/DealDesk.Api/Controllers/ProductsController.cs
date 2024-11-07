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

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductRequest productDto, CancellationToken ct = default)
        {
            var createdProductId = await _productService.Create(productDto, ct);
            var createdProduct = await _productService.GetById(createdProductId, ct);
            return CreatedAtAction(nameof(GetById), new { productId = createdProductId }, createdProduct);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct = default)
        {
            var products = await _productService.GetAll(ct);
            return Ok(products);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetById(long productId, CancellationToken ct = default)
        {
            var product = await _productService.GetById(productId, ct);
            return Ok(product);
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> Update(long productId, ProductRequest updatedProductDto, CancellationToken ct = default)
        {
            await _productService.Update(productId, updatedProductDto, ct);
            return NoContent();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(long productId, CancellationToken ct = default)
        {
            await _productService.Delete(productId, ct);
            return NoContent();
        }

        [HttpGet("discount")]
        public async Task<IActionResult> GetDiscountedPrice([FromQuery] ProductDiscountRequest request, CancellationToken ct = default)
        {
            // Calculate the discounted price using the ProductService
            var response = await _productService.GetDiscountedPrice(request.ProductId, request.Quantity, request.CustomerId, ct);

            // Return the discounted price
            return Ok(response);
        }
    }
}
