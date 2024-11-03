using DealDesk.Business.Interfaces;
using DealDesk.DataAccess.Entities;

namespace DealDesk.Business
{
    public class PriceCalculator
    {
        private readonly IDiscountStrategy _discountStrategy;

        public PriceCalculator(IDiscountStrategy discountStrategy)
        {
            _discountStrategy = discountStrategy;
        }

        public decimal CalculatePrice(Product product)
        {
            return _discountStrategy.ApplyDiscount(product.StandardPrice);
        }
    }

}
