namespace MasterFinder.Domain.Exceptions
{
    public class NotFoundException : DomainException
    {
        public NotFoundException(string entityName, int id)
            : base($"{entityName} с ID {id} не найден", "NOT_FOUND")
        {
        }
    }
}