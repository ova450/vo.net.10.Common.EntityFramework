
namespace EntityNexus.DomainModel;

/// <summary>
/// Маркерный интерфейс для сущностей, имеющих родителя.
/// Свойства ParentId и Parent настраиваются через EntityTypeConfiguration.
/// </summary>
public interface IHasParent<TParent, TKey>
    where TParent : IEntity<TKey>
    where TKey : IEquatable<TKey>;

/// <summary>
/// Упрощённая версия для int-ключа.
/// </summary>
public interface IHasParent<TParent> : IHasParent<TParent, int>
    where TParent : IEntity;