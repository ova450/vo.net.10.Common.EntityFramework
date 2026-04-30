using CORM.Core.Domain.Model;

namespace CORM.Core.Domain.Model
{
    public interface IChild<TChild> where TChild : IEntityBase
    {
        ICollection<TChild> Children { get; set; }
    }

    public interface IChildren<TChild1, TChild2>
        where TChild1 : IEntityBase
        where TChild2 : IEntityBase
    {
        ICollection<TChild1> Children1 { get; set; }
        ICollection<TChild2> Children2 { get; set; }
    }
}
