namespace CORM.Core.Domain.Model;

public interface IParent<TParent, TKey>
    where TParent : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    TKey ParentId { get; set; }
    TParent? Parent { get; set; }
}

public interface IParent<TParent> : IParent<TParent, int> where TParent : IEntity<int>;
