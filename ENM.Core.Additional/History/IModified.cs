using EntityNexus.DomainModel;

namespace EntityNexus.Additionals.History;

/// <summary>
/// Сущность истории изменений (отдельная таблица).
/// Используется для хранения истории модификаций основной сущности.
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
    TUser? ModifiedByUser { get; set; }     // явное навигационное свойство
}

/// <summary>
/// Упрощённая версия для наиболее частого случая (int ключи).
/// </summary>
public interface IModified<TParent, TUser>
    : IModified<int, TParent, int, TUser, int>
    where TParent : IEntity<int>
    where TUser : IEntity<int>;

/// <summary>
/// Самая простая версия (когда пользователь тоже int).
/// </summary>
public interface IModified<TParent>
    : IModified<int, TParent, int, IEntity<int>, int>
    where TParent : IEntity<int>;