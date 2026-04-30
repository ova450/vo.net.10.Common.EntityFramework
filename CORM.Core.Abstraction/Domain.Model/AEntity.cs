using CORM.Core.Domain.Model;

namespace CORM.Core.Abstraction.Domain.Model;

public abstract class AEntity<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
{
    public required TKey Id { get; set; }
}

public abstract class AEntity : AEntity<int> { }
