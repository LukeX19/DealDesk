using DealDesk.Business.Interfaces;

namespace DealDesk.Business.Decorators
{
    public class SpecialOfferDiscountDecorator : DiscountDecorator
    {
        public SpecialOfferDiscountDecorator(IDiscountStrategy innerDiscount)
            : base(innerDiscount) { }

        public override decimal ApplyDiscount(decimal price)
        {
            // Apply inner discount first, then add a special offer discount
            var discountedPrice = base.ApplyDiscount(price);
            // Additional 5% off
            return discountedPrice * 0.95M;
        }
    }
}
