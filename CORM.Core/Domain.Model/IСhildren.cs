namespace CORM.Core.Domain.Model
{
    public interface IChild<TChild> where TChild : IEntity
    {
        ICollection<TChild> Children { get; set; }
    }

    public interface IChildren<TChild1, TChild2>
        where TChild1 : IEntity
        where TChild2 : IEntity
    {
        ICollection<TChild1> Children1 { get; set; }
        ICollection<TChild2> Children2 { get; set; }
    }
}
