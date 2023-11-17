namespace SemSnel.Portofolio.Domain._Common.ValueObjects;

/// <summary>
/// Value object base class.
/// Value objects are immutable objects that are compared by their properties.
/// They are not persisted in the database.
/// For more information see https://enterprisecraftsmanship.com/posts/value-object-better-implementation//.
/// </summary>
public abstract class ValueObject :
    IEquatable<ValueObject>
{
    /// <summary>
    /// Gets the equality components. These are the properties that are used to compare value objects.
    /// </summary>
    /// <returns> The equality components. </returns>
    public abstract IEnumerable<object> GetEqualityComponents();

    /// <summary>
    /// Compares two value objects.
    /// </summary>
    /// <param name="obj"> The other value object. </param>
    /// <returns>Returns true if the value objects are equal.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        var valueObject = (ValueObject)obj;

        return GetEqualityComponents()
            .SequenceEqual(valueObject.GetEqualityComponents());
    }

    public static bool operator ==(ValueObject left, ValueObject right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(ValueObject left, ValueObject right)
    {
        return !Equals(left, right);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }

    /// <summary>
    /// Compares two value objects.
    /// </summary>
    /// <param name="other"> The other value object. </param>
    /// <returns> Returns true if the value objects are equal. </returns>
    public bool Equals(ValueObject? other)
    {
        return Equals((object?)other);
    }
}