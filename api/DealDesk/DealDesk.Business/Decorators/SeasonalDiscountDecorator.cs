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
            return discountedPrice * 0.85M; // Applies a 15% seasonal discount
        }
    }
}
