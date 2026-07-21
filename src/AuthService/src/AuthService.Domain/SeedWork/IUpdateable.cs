namespace AuthService.Domain.SeedWork
{
    /// <summary>
    /// Marks an entity as updateable and allows the changes in the entity to be saved in the database.
    /// </summary>
    public interface IUpdateable
    {
        DateTime? UpdatedDate { get; }
    }
}
