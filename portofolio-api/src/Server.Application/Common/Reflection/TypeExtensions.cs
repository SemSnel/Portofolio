namespace SemSnel.Portofolio.Server.Application.Common.Reflection;

/// <summary>
/// Extensions for the <see cref="Type"/> class.
/// </summary>
public static class TypeExtensions
{
    /// <summary>
    /// Checks if a type inherits or implements another type even if it is a generic type.
    /// </summary>
    /// <param name="child"> The child type. </param>
    /// <param name="parent"> The parent type. </param>
    /// <returns> True if the child inherits or implements the parent. </returns>
    public static bool InheritsOrImplements(this Type child, Type parent)
    {
        parent = ResolveGenericTypeDefinition(parent);

        var currentChild = child.IsGenericType
            ? child.GetGenericTypeDefinition()
            : child;

        while (currentChild != typeof(object))
        {
            if (parent == currentChild || HasAnyInterfaces(parent, currentChild))
            {
                return true;
            }

            currentChild = currentChild.BaseType != null
                           && currentChild.BaseType.IsGenericType
                ? currentChild.BaseType.GetGenericTypeDefinition()
                : currentChild.BaseType;

            if (currentChild == null)
            {
                return false;
            }
        }

        return false;
    }

    private static bool HasAnyInterfaces(Type parent, Type child)
    {
        return child.GetInterfaces()
            .Any(childInterface =>
            {
                var currentInterface = childInterface.IsGenericType
                    ? childInterface.GetGenericTypeDefinition()
                    : childInterface;

                return currentInterface == parent;
            });
    }

    private static Type ResolveGenericTypeDefinition(Type parent)
    {
        var shouldUseGenericType = true;

        if (parent.IsGenericType && parent.GetGenericTypeDefinition() != parent)
        {
            shouldUseGenericType = false;
        }

        if (parent.IsGenericType && shouldUseGenericType)
        {
            parent = parent.GetGenericTypeDefinition();
        }

        return parent;
    }
}