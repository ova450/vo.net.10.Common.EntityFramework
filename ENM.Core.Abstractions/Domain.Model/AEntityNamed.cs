using EntityNexus.DomainModel;

namespace EntityNexus.Abstractions.Domain.Model;

public abstract class AEntityNamed<TKey>(string name) : AEntity<TKey> where TKey : IEquatable<TKey>
{
    public required string Name { get; set; } = name;
}

public abstract class AEntityNamed(string name) : AEntityNamed<int>(name);
