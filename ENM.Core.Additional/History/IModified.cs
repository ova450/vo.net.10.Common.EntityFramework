using EntityNexus.DomainModel;

namespace EntityNexus.Additionals.History;

/// <summary>
/// Сущность истории изменений (отдельная таблица).
/// </summary>
public interface IModified<TId, TParent, TParentId, TUser, TUserId>
    : IEntity<TId>, IHasParent<TParent, TParentId>
    where TId : IEquatable<TId>
    where TParent : IEntity<TParentId>
    where TParentId : IEquatable<TParentId>
    where TUser : IEntity<TUserId>
    where TUserId : IEquatable<TUserId>
{
    DateTimeOffset ModifiedAt { get; set; }
    TUserId ModifiedBy { get; set; }
    TUser? ModifiedByUser { get; set; }
}

public interface IModified<TParent, TUser>
    : IModified<int, TParent, int, TUser, int>
    where TParent : IEntity<int>
    where TUser : IEntity<int>;