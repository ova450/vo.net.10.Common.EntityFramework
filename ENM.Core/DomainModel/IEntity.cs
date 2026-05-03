namespace EntityNexus.DomainModel;

/// <summary>
/// Базовый интерфейс для всех сущностей с идентификатором.
/// </summary>
public interface IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    TKey Id { get; set; }
}

/// <summary>
/// Упрощённая версия для наиболее распространённого случая (int Id).
/// </summary>
public interface IEntity : IEntity<int>;
