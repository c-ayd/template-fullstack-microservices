namespace AuthService.Domain.SeedWork
{
    /// <summary>
    /// Marks an entity as soft deleteable and updates <see cref="ISoftDelete.IsDeleted"/> and <see cref="ISoftDelete.DeletedDate"/>
    /// instead of deleting the entity from the database.
    /// <para><see cref="ISoftDelete.SoftDelete"/> method should be called to prepare the entity for soft deletion.</para>
    /// </summary>
    public interface ISoftDelete
    {
        bool IsDeleted { get; }
        DateTime? DeletedDate { get; }

        /// <summary>
        /// Sets necessary properties to their default values to prepare the entity for soft deletion.
        /// </summary>
        void SoftDelete();
    }
}