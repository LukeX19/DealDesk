using DealDesk.Business.Interfaces;

namespace DealDesk.Business.Strategies
{
    public class VolumeDiscount : IDiscountStrategy
    {
        public decimal ApplyDiscount(decimal price)
        {
            // Example: 10% off for volume discount
            return price * 0.9M;
        }
    }
}
