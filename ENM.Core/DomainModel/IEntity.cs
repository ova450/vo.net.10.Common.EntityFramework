namespace EntityNexus.DomainModel;

/// <summary>
/// Базовый интерфейс сущности.
/// </summary>
public interface IEntity<TKey> where TKey : IEquatable<TKey> { TKey Id { get; set; } }

public interface IEntity { int Id { get; set; } }