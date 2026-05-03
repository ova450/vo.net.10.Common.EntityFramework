using EntityNexus.DomainModel;

namespace EntityNexus.Additionals.History;

public interface IDeleted<TUser, TUserId> : IHasParent<TUser, TUserId>
    where TUserId : IEquatable<TUserId>
    where TUser : IEntity<TUserId>
{
    DateTimeOffset DeletedAt { get; set; }
    TUserId DeletedBy { get; set; }
    TUser? DeletedByUser { get; set; }
}

public interface IDeleted<TUser> : IDeleted<TUser, int> where TUser : IEntity<int>;