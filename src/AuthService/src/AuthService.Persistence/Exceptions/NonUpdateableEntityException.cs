using AuthService.Domain.SeedWork;

namespace AuthService.Persistence.Exceptions
{
    public class NonUpdateableEntityException : Exception
    {
        public NonUpdateableEntityException(string entityType)
            : base($"{entityType} does not implement {nameof(IUpdateable)} interface. Therefore, the entity cannot be updated.")
        {
        }
    }
}
