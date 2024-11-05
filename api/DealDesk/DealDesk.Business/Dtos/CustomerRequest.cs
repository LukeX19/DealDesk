namespace DealDesk.Business.Dtos
{
    public class CustomerRequest
    {
        public string Name { get; set; }
        public ICollection<string> DiscountStrategies { get; set; }
    }
}
