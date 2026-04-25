using CORM.Core.Entities;

namespace CORM.Core.Navigation;

/// <summary>
/// Navigation property for child entities (one-to-many)
/// </summary>
/// <typeparam name="TChild">Type of child entity</typeparam>
public interface IHasChildren<TChild> where TChild : IEntityBase
{
    ICollection<TChild> Children { get; set; }
}

/// <summary>
/// Navigation property for multiple child entity types
/// </summary>
public interface IHasMultipleChildren<TChild1, TChild2>
    where TChild1 : IEntityBase
    where TChild2 : IEntityBase
{
    ICollection<TChild1> Children1 { get; set; }
    ICollection<TChild2> Children2 { get; set; }
}

// Добавьте другие комбинации по необходимости, но лучше использовать композицию