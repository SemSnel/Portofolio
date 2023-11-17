using System.Reflection;

namespace SemSnel.Portofolio.Domain._Common.Enumerations;

public abstract class BaseEnumeration : IComparable
{
    private readonly int _id;
    private readonly string _name;

    protected BaseEnumeration(int id, string name)
    {
        _id = id;
        _name = name;
    }

    public int Id => _id;
    public string Name => _name;

    public override string ToString() => Name;
    
    public static IEnumerable<T> GetAll<T>()
        where T : BaseEnumeration
    {
        // get all public static field types with type T
        var fields = typeof(T).GetFields(
            BindingFlags.Public |
            BindingFlags.DeclaredOnly |
            BindingFlags.Static);

        var values = fields.Select(info => info.GetValue(default)).Cast<T>();

        return values;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not BaseEnumeration otherValue)
        {
            return false;
        }

        var typeMatches = GetType() == obj.GetType();
        
        var valueMatches = Id.Equals(otherValue.Id);

        return typeMatches && valueMatches;
    }

    public int CompareTo(object? other) => Id.CompareTo(((BaseEnumeration)other!).Id);
    
    public override int GetHashCode() => Id.GetHashCode();
}

public abstract class BaseEnumeration<T> : BaseEnumeration
    where T : BaseEnumeration
{
    protected BaseEnumeration(int id, string name) : base(id, name)
    {
    }

    public static IEnumerable<T> GetAll()
    {
        return BaseEnumeration.GetAll<T>();
    }
}