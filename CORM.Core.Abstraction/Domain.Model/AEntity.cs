using CORM.Core.Domain.Model;

namespace CORM.Core.Abstraction.Domain.Model;

public abstract class AEntityBase<TKey> : IEntityBase<TKey>
    where TKey : IEquatable<TKey>
{
    public required TKey Id { get; set; }
}

public abstract class AEntityBase : AEntityBase<int> { }

public abstract class AEntity<TKey>(string name) : AEntityBase<TKey> 
    where TKey : IEquatable<TKey>  
{
    public string Name { get; set; } = name; 
}

public abstract class AEntity(string name) : AEntity<int>(name) { }