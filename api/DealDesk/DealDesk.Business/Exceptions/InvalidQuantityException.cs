namespace DealDesk.Business.Exceptions
{
    public class InvalidQuantityException : Exception
    {
        public InvalidQuantityException()
            : base("Invalid product quantity value. Quantity must be greater than zero.")
        {
        }
    }
}
