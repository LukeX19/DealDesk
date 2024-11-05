namespace DealDesk.DataAccess.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<string> DiscountStrategies { get; set; }
    }
}
