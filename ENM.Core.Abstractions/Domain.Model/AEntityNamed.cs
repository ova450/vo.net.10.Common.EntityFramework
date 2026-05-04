
namespace EntityNexus.Abstractions.Domain.Model;

public abstract class AEntityNamed<TKey> : AEntity<TKey> where TKey : IEquatable<TKey>
{
    public required string Name { get; set; }
}

public abstract class AEntityNamed : AEntityNamed<int>;
