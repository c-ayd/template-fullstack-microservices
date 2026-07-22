#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace AuthService.Domain.SeedWork
{
    /// <summary>
    /// Base class for entities to handle the ID and created timestamp creations automatically.
    /// </summary>
    /// <typeparam name="T">Type of the primary key</typeparam>
    public abstract class EntityBase<T> : IEquatable<EntityBase<T>>
        where T : notnull
    {
        public T Id { get; init; }
        public DateTimeOffset CreatedDate { get; init; }

        // Reserved for EF Core
        protected EntityBase()
        {
        }

        public EntityBase(T id)
        {
            Id = id;
            CreatedDate = DateTimeOffset.UtcNow;
        }

        public bool Equals(EntityBase<T>? other)
        {
            if (other is null)
                return false;

            return Id.Equals(other.Id);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not EntityBase<T> other)
                return false;

            return Equals(other);
        }

        public static bool operator ==(EntityBase<T>? left, EntityBase<T>? right)
        {
            if (left is null && right is null)
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(EntityBase<T>? left, EntityBase<T>? right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
