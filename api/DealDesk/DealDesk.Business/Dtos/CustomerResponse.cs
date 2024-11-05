namespace DealDesk.Business.Dtos
{
    public class CustomerResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<string> DiscountStrategies { get; set; }
    }
}
