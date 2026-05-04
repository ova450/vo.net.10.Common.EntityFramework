using EntityNexus.DomainModel;

namespace EntityNexus.Abstractions.Domain.Model;

public abstract class AEntity<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; } = default!;
}

public abstract class AEntity : IEntity// where TKey : IEquatable<TKey>
{
    public int Id { get; set; }
}