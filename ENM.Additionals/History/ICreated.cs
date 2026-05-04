using EntityNexus.DomainModel;

namespace EntityNexus.Additionals.History;

public interface ICreated { DateTimeOffset CreatedAt { get; set; } }

public interface ICreated<TUser, TUserId> : ICreated, IHasParent<TUser, TUserId>
    where TUserId : IEquatable<TUserId>
    where TUser : IEntity<TUserId>
{
    TUserId CreatedBy { get; set; }
    TUser? CreatedByUser { get; set; }
}

public interface ICreated<TUser> : ICreated<TUser, int> where TUser : IEntity<int>;

