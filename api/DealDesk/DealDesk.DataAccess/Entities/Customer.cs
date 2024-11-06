namespace DealDesk.DataAccess.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public IList<string> DiscountStrategies { get; set; } = new List<string>();
    }
}
