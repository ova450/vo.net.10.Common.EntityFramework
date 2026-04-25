using CORM.Core.Entities;

namespace CORM.Core.Navigation;

/// <summary>
/// Navigation property for parent entity (many-to-one)
/// </summary>
/// <typeparam name="TParent">Type of parent entity</typeparam>
public interface IHasParent<TParent> where TParent : IEntityBase
{
    int ParentId { get; set; }
    TParent? Parent { get; set; }
}

/// <summary>
/// Navigation property for multiple parent entities
/// </summary>
public interface IHasMultipleParents<TParent1, TParent2>
    where TParent1 : IEntityBase
    where TParent2 : IEntityBase
{
    int Parent1Id { get; set; }
    TParent1? Parent1 { get; set; }
    
    int Parent2Id { get; set; }
    TParent2? Parent2 { get; set; }
}