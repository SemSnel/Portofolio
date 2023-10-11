using System.Collections.ObjectModel;
using SemSnel.Portofolio.Domain.Common.Entities;

namespace SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.PubSub.Dictionaries;

public class MessageTypeDictionary : 
    ReadOnlyDictionary<string, Type>, 
    IMessageTypeDictionary
{
    public MessageTypeDictionary(IDictionary<string, Type> dictionary) : base(dictionary)
    {
        foreach (var type in dictionary.Values)
        {
            if (!typeof(EventBase).IsAssignableFrom(type))
            {
                throw new ArgumentException($"Type {type.Name} is not a subclass of {nameof(EventBase)}");
            }
            
            if (type.IsAbstract || type.IsInterface || type.IsGenericType)
            {
                throw new ArgumentException($"Type {type.Name} cannot be interface, abstract or generic");
            }
        }
    }
    
    public string GetKey(Type type)
    {
        return this.FirstOrDefault(x => x.Value == type).Key;
    }
    
    public bool TryGetKey(Type type, out string key)
    {
        var result = this.FirstOrDefault(x => x.Value == type);
        
        key = result.Key;

        return result.Key != null;
    }
}