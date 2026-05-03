using EntityNexus.DomainModel;

namespace EntityNexus.Additionals.History;

/// <summary>
/// Интерфейс для сущностей, хранящих информацию о действиях со СТРОКОЙ(!).
/// </summary>
public interface IRecordAuditable<TUser, TUserId> : IHasParent<TUser, TUserId>
    where TUserId : IEquatable<TUserId>
    where TUser : IEntity<TUserId>
{
    DateTimeOffset DoneAt { get; set; }
    TUserId DoneBy { get; set; }
}

public interface IExistenceAuditable<TCreator, TCreatorId, TDeleter, TDeleterId>
where TCreatorId : IEquatable<TCreatorId>
where TCreator : IEntity<TCreatorId>
where TDeleterId : IEquatable<TDeleterId>
where TDeleter : IEntity<TDeleterId>
{
    IRecordAuditable<TCreator, TCreatorId> CreatedRecord { get; set; }
    IRecordAuditable<TDeleter, TDeleterId>? DeletedRecord { get; set; }
}

public interface IExistenceAuditable<TCreator, TCreatorId>
where TCreatorId : IEquatable<TCreatorId>
where TCreator : IEntity<TCreatorId>
{
    IRecordAuditable<TCreator, TCreatorId> CreatedRecord { get; set; }
}

public interface IExistenceAuditable<TCreator> : IExistenceAuditable<TCreator, int>
where TCreator : IEntity;

