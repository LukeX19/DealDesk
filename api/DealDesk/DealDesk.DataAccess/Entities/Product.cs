namespace DealDesk.DataAccess.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal StandardPrice { get; set; }
    }
}
