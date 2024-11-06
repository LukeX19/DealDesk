using DealDesk.Business.Interfaces;

namespace DealDesk.Business.Strategies
{
    public class SeasonalDiscount : IDiscountStrategy
    {
        public decimal ApplyDiscount(decimal price)
        {
            // Example: 15% off for seasonal discount
            return price * 0.85M;
        }
    }
}
