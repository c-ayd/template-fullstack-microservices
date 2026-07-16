namespace AuthService.Persistence.Exceptions
{
    public class SeedDataEntryNotFoundException : Exception
    {
        public SeedDataEntryNotFoundException(string message) : base(message)
        {
        }
    }
}