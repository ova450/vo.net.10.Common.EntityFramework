namespace EntityNexus.DomainModel;

/// <summary>
/// Интерфейс для сильного типизирования ключа.
/// </summary>
public interface IKey<T> where T : IEquatable<T>
{
    T Value { get; }
}

// Зачем мне нужно IHasKey, если у меня уже есть IEntity<TKey>? Потому что IHasKey может использоваться для сущностей, которые не являются полноценными IEntity, но все же хотят иметь сильный типизированный ключ. Это может быть полезно в случаях, когда вам нужно работать с объектами, которые не требуют всех возможностей IEntity, но все же хотят иметь уникальный идентификатор.

/// <summary>
/// Маркер для сущностей, использующих IKey.
/// </summary>
public interface IHasKey<TKey> : IEntity<TKey> where TKey : IKey<TKey>, IEquatable<TKey>;