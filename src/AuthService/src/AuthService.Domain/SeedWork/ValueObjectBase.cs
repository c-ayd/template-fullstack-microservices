namespace AuthService.Domain.SeedWork
{
    /// <summary>
    /// Base class for value objects to handle property-based comparisons.
    /// </summary>
    public abstract class ValueObjectBase : IEquatable<ValueObjectBase>
    {
        public abstract IEnumerable<object?> GetEqualityComponents();

        public bool Equals(ValueObjectBase? other)
        {
            if (other is null)
                return false;

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override bool Equals(object? obj)
        {
            if (obj is not ValueObjectBase other)
                return false;

            return Equals(other);
        }

        public static bool operator ==(ValueObjectBase? left, ValueObjectBase? right)
        {
            if (left is null && right is null)
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(ValueObjectBase? left, ValueObjectBase? right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }
    }
}
