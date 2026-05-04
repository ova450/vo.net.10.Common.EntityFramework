using EntityNexus.DomainModel;

namespace EntityNexus.Additionals.History;

public interface IDeleted { DateTimeOffset? DeletedAt { get; set; } }

public interface IDeleted<TUser, TUserId> : IDeleted, IHasParent<TUser, TUserId>
    where TUserId : IEquatable<TUserId>
    where TUser : IEntity<TUserId>
{
    TUserId DeletedBy { get; set; }
    TUser? DeletedByUser { get; set; }
}

public interface IDeleted<TUser> : IDeleted<TUser, int> where TUser : IEntity<int>;