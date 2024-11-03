using DealDesk.Business.Interfaces;

namespace DealDesk.Business.Strategies
{
    public class NoDiscount : IDiscountStrategy
    {
        public decimal ApplyDiscount(decimal price)
        {
            return price;
        }
    }
}
