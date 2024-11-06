using System.ComponentModel.DataAnnotations;

namespace DealDesk.Business.Dtos
{
    public class CustomerRequest
    {
        [Required]
        public string Name { get; set; }
        public IList<string> DiscountStrategies { get; set; } = new List<string>();
    }
}
