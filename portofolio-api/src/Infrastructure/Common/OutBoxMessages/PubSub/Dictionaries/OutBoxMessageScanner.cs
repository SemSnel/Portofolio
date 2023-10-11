using System.Reflection;
using SemSnel.Portofolio.Application.Common.Reflection;
using SemSnel.Portofolio.Domain.Common.Entities;
using SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.Persistence.Entities;

namespace SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.PubSub.Dictionaries;

public sealed class OutBoxMessageTypeScanner
{
    public IDictionary<string, Type> Scan(Assembly assembly)
    {
        var types = assembly.GetTypes()
            .Where(type => 
                        type is { IsClass: true, IsAbstract: false, IsPublic: true } 
                        && type.InheritsOrImplements(typeof(EventBase)))
            .ToList();

        var dictionary = new Dictionary<string, Type>();

        foreach (var type in types)
        {
            var name = type.Name;
            
            dictionary.Add(name, type);
        }

        return dictionary;
    }
}