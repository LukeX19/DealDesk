using System.ComponentModel.DataAnnotations;

namespace DealDesk.Business.Dtos
{
    public class ProductDiscountRequest
    {
        [Required]
        public long ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Product Quantity must be greater than zero.")]
        public int Quantity { get; set; }

        [Required]
        public long CustomerId { get; set; }
    }
}
