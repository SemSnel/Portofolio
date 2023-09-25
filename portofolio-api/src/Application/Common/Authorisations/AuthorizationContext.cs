namespace SemSnel.Portofolio.Application.Common.Authorisations;

public class AuthorizationContext<T>
{
    public T Item { get; set; }
    
    public AuthorizationContext(T item)
    {
        Item = item;
    }
}