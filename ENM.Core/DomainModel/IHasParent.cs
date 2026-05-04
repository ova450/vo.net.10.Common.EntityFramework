
namespace EntityNexus.DomainModel;

/// <summary>
/// Маркерный интерфейс для сущностей, имеющих родителя.
/// Свойства ParentId и Parent настраиваются через EntityTypeConfiguration.
/// </summary>
public interface IHasParent<TParent, TKey>
    where TParent : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    TKey ParentId { get; set; }
    TParent? Parent { get; set; }
}

/// <summary>
/// Упрощённая версия для int-ключа.
/// </summary>
public interface IHasParent<TParent> where TParent : IEntity
{
    int ParentId { get; set; }
    TParent? Parent { get; set; }
}