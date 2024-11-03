namespace DealDesk.DataAccess.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        private const string MessageTemplate = "The {0} with id {1} was not found.";

        public EntityNotFoundException(string entityName, long id)
            : base(string.Format(MessageTemplate, entityName, id))
        {
        }
    }
}
