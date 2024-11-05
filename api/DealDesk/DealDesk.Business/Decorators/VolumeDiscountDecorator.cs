using DealDesk.Business.Interfaces;

namespace DealDesk.Business.Decorators
{
    public class VolumeDiscountDecorator : DiscountDecorator
    {
        public VolumeDiscountDecorator(IDiscountStrategy innerDiscount)
            : base(innerDiscount) { }

        public override decimal ApplyDiscount(decimal price)
        {
            var discountedPrice = base.ApplyDiscount(price);
            return discountedPrice * 0.9M; // Applies a 10% volume discount
        }
    }
}
