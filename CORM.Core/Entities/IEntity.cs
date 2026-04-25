namespace CORM.Core.Entities;

/// <summary>
/// Base entity with identifier
/// </summary>
public interface IEntityBase 
{ 
    int Id { get; init; } 
}

/// <summary>
/// Entity with name property
/// </summary>
public interface IEntity : IEntityBase 
{ 
    string Name { get; set; } 
}

/// <summary>
/// Entity with timing information
/// </summary>
public interface IEntityTiming
{
    IDoneTiming Created { get; init; }
    ICollection<IDoneTiming> Modified { get; init; }
}

/// <summary>
/// Entity with additional details
/// </summary>
public interface IEntityDetails
{
    string? Description { get; init; }
    string? LogoUrl { get; init; }
    string? WebsiteUrl { get; init; }
}

/// <summary>
/// Timing information for an action
/// </summary>
public interface IDoneTiming
{
    DateTimeOffset Done { get; init; }
    int DoneBy { get; init; }
}