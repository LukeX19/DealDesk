namespace DealDesk.Business.Interfaces
{
    public interface IDiscountStrategy
    {
        decimal ApplyDiscount(decimal price);
    }
}
