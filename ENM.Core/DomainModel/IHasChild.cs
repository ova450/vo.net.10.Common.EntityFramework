
namespace EntityNexus.DomainModel;

/// <summary>
/// Маркерный интерфейс для сущностей, являющихся родителями (имеют дочерние элементы).
/// Коллекция Children настраивается в EntityTypeConfiguration.
/// </summary>
public interface IHasChild<TChild, TKey>
    where TKey : IEquatable<TKey>
    where TChild : IEntity<TKey>
{ 
    ICollection<TChild> Children { get; set; }
}

/// <summary>
/// Упрощённая версия для int-ключа.
/// </summary>
public interface IHasChild<TChild> where TChild : IEntity
{
    ICollection<TChild> Children { get; set; }
}