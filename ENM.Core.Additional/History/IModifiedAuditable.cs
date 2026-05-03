using EntityNexus.DomainModel;

namespace EntityNexus.Additionals.History;

public interface IModifiedAuditable<TKey, TParent, TParentKey>
    : IEntity<TKey>, IHasParent<TParent, TParentKey>
    where TKey : IEquatable<TKey>
    where TParentKey : IEquatable<TParentKey>
    where TParent : IEntity<TParentKey>;

public interface IModifiedAuditable<TKey, TParent>
    :IModifiedAuditable<TKey, TParent, TKey>
    where TKey : IEquatable<TKey>
    where TParent : IEntity<TKey>;

public interface IModifiedAuditable<TParent>
    : IModifiedAuditable<int, TParent, int>
    where TParent : IEntity;
