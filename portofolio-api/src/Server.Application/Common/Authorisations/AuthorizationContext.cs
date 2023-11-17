namespace SemSnel.Portofolio.Server.Application.Common.Authorisations;

/// <summary>
/// A context that can be used to authorise an item.
/// </summary>
/// <typeparam name="T"> The type. </typeparam>
public class AuthorizationContext<T>
{
    /// <summary>
    /// Gets or sets the item.
    /// </summary>
    public T Item { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorizationContext{T}"/> class.
    /// </summary>
    /// <param name="item"> The item. </param>
    public AuthorizationContext(T item)
    {
        Item = item;
    }
}