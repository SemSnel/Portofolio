namespace SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.PubSub.Dictionaries;

public interface IMessageTypeDictionary : IReadOnlyDictionary<string, Type>
{
    public string GetKey(Type type);
    public bool TryGetKey(Type type, out string key);
}