namespace CORM.Core.Domain.Model
{
    public interface IEntity<TKey> where TKey : IEquatable<TKey> { TKey Id { get; set; } }
    public interface IEntity : IEntity<int>;
    public interface IEntityNamed : IEntity { string Name { get; set; } }

    public interface IEntityCreated
    {
        DateTimeOffset CreatedAt { get; set; }
        int CreatedBy { get; set; }
    }

    public interface IEntityModified
    {
        DateTimeOffset? ModifiedAt { get; set; }
        int? ModifiedBy { get; set; }
    }

    public interface IEntityDeleted
    {
        bool IsDeleted { get; set; }
        DateTimeOffset? DeletedAt { get; set; }
        int? DeletedBy { get; set; }
    }

    public interface IConcurrency
    {
        byte[] RowVersion { get; set; }
    }


    public interface IEntityDetails
    {
        string? Description { get; init; }
        string? LogoUrl { get; init; }
        string? WebsiteUrl { get; init; }
    }
}