namespace EntityNexus.DomainModel;

/// <summary>
/// Интерфейс для сильного типизирования ключа.
/// </summary>
public interface IKey<T> where T : IEquatable<T>
{
    T Value { get; }
}

/// <summary>
/// Маркер для сущностей, использующих IKey.
/// </summary>
public interface IHasKey<TKey> : IEntity<TKey> where TKey : IKey<TKey>;