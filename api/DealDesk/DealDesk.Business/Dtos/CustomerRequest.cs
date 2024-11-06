namespace DealDesk.Business.Dtos
{
    public class CustomerRequest
    {
        public string Name { get; set; }
        public IList<string> DiscountStrategies { get; set; } = new List<string>();
    }
}
