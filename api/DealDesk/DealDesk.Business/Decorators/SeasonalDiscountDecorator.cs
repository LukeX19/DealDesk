using DealDesk.Business.Interfaces;

namespace DealDesk.Business.Decorators
{
    public class SeasonalDiscountDecorator : DiscountDecorator
    {
        public SeasonalDiscountDecorator(IDiscountStrategy innerDiscount)
            : base(innerDiscount) { }

        public override decimal ApplyDiscount(decimal price)
        {
            var discountedPrice = base.ApplyDiscount(price);

            // Example: 15% off for seasonal discount (SeasonalDiscount Strategy)
            return discountedPrice * 0.85M;
        }
    }
}
