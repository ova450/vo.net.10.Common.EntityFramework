namespace CORM.Core.Domain.Model
{
    public interface IParent<TParent, TKey> 
        where TParent : IEntityBase<TKey>
        where TKey : IEquatable<TKey>
    {
        TKey ParentId { get; set; }
        TParent? Parent { get; set; }
    }

    public interface IParent<TParent> 
        where TParent : IEntityBase
    {
        int ParentId { get; set; }
        TParent? Parent { get; set; }
    }

    public interface IParentsBase<TParent1, TParent2>
        where TParent1 : IEntityBase
        where TParent2 : IEntityBase
    {
        TParent1? Parent1 { get; set; }
        TParent2? Parent2 { get; set; }
    }

    public interface IParents<TParent1, TParent2, TKey1, TKey2> : IParentsBase<TParent1, TParent2>
        where TParent1 : IEntityBase
        where TParent2 : IEntityBase
    {
        TKey1 Parent1Id { get; set; }
        TKey2 Parent2Id { get; set; }
    }

    public interface IParents<TParent1, TParent2, TKey> : IParentsBase<TParent1, TParent2>
        where TParent1 : IEntityBase
        where TParent2 : IEntityBase
    {
        TKey Parent1Id { get; set; }
        TKey Parent2Id { get; set; }
    }

    public interface IParents<TParent1, TParent2> : IParentsBase<TParent1, TParent2>
        where TParent1 : IEntityBase
        where TParent2 : IEntityBase
    {
        int Parent1Id { get; set; }
        int Parent2Id { get; set; }
    }
}