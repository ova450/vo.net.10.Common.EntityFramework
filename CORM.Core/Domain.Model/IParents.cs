namespace CORM.Core.Domain.Model;

public interface IParents<TParent1, TParent2, TKey1, TKey2>
    where TParent1 : IEntity<TKey1>
    where TParent2 : IEntity<TKey2>
    where TKey1 : IEquatable<TKey1>
    where TKey2 : IEquatable<TKey2>
{
    TKey1 ParentId1 { get; set; }
    IEntity<TKey1> ParentEntity1 { get; set; }
    TKey2 ParentId2 { get; set; }   
    IEntity<TKey2> ParentEntity2 { get; set; }
}

public interface IParents<TParent1, TParent2, TKey> : IParents<TParent1, TParent2, TKey, TKey>
    where TParent1 : IEntity<TKey>
    where TParent2 : IEntity<TKey>
    where TKey : IEquatable<TKey>
    ;

public interface IParents<TParent1, TParent2> : IParents<TParent1, TParent2, int>
    where TParent1 : IEntity<int>
    where TParent2 : IEntity<int>
    ;
