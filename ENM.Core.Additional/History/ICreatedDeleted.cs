using EntityNexus.DomainModel;

namespace EntityNexus.Additionals.History;

public interface ICreated<TCreator, TCreatorId>
where TCreatorId : IEquatable<TCreatorId>
where TCreator : IEntity<TCreatorId>
{ 
    DateTimeOffset CreatedAt { get; set; }
    TCreatorId CreatedBy { get; set; }
    string? CreatedByClassName { get; set; } // = typeof(TCreator).Name;   в реализации
    string? CreatedByName { get; set; } // = TCreator.FirstOrDefault(CreatedBy).Name;   в реализации
}

public interface ICreated<TCreator> : ICreated<TCreator, IKeyDefault>
where TCreator : IEntity;

public interface IDeleted<TDeletor, TDeletorId>
where TDeletorId : IEquatable<TDeletorId>
where TDeletor : IEntity<TDeletorId>
{
    DateTimeOffset DeletedAt { get; set; }
    TDeletorId DeletedBy { get; set; }
    string? DeletedByClassName { get; set; } // = typeof(TDeletor).Name;   в реализации
    string? DeletedByName { get; set; } // = TDeletor.FirstOrDefault(DeletedBy).Name;   в реализации
}

public interface IDeleted<TDeletor> : IDeleted<TDeletor, IKeyDefault>
where TDeletor : IEntity;