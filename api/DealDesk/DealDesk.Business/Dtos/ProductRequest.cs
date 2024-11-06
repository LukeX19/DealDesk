using System.ComponentModel.DataAnnotations;

namespace DealDesk.Business.Dtos
{
    public class ProductRequest
    {
        [Required]
        public string Name { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Standard price must be greater than zero.")]
        public decimal StandardPrice { get; set; }
    }
}
