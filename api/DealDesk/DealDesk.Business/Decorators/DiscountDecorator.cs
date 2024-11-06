using DealDesk.Business.Interfaces;

namespace DealDesk.Business.Decorators
{
    public class DiscountDecorator : IDiscountStrategy
    {
        private readonly IDiscountStrategy _innerDiscount;

        public DiscountDecorator(IDiscountStrategy innerDiscount)
        {
            _innerDiscount = innerDiscount;
        }

        public virtual decimal ApplyDiscount(decimal price)
        {
            return _innerDiscount.ApplyDiscount(price);
        }
    }

}
