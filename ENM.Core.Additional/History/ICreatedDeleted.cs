using EntityNexus.DomainModel;

namespace EntityNexus.Additionals.History;

public interface ICreated<TUser, TUserId> : IHasParent<TUser, TUserId>
where TUserId : IEquatable<TUserId>
where TUser : IEntity<TUserId>
{
    DateTimeOffset CreatedAt { get; set; }
}

public interface ICreated<TUser> : ICreated<TUser, int>
where TUser : IEntity;

public interface IDeleted<TUser, TUserId> : IHasParent<TUser, TUserId>
where TUserId : IEquatable<TUserId>
where TUser : IEntity<TUserId>
{
    DateTimeOffset DeletedAt { get; set; }
}

public interface IDeleted<TUser> : IDeleted<TUser, int>
where TUser : IEntity;