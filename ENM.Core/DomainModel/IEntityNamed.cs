namespace EntityNexus.DomainModel;

/// <summary>
/// Интерфейс для сущностей, имеющих обязательное имя.
/// </summary>
public interface IEntityNamed<TKey> : IEntity<TKey>    where TKey : IEquatable<TKey>
{
    string Name { get; set; }
}

/// <summary>
/// Упрощённая версия для int-ключа.
/// </summary>
public interface IEntityNamed : IEntity;