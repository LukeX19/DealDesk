namespace DealDesk.Business.Dtos
{
    public class ProductDiscountResponse
    {
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public long CustomerId { get; set; }
        public decimal DiscountedTotalPrice { get; set; }
    }
}
