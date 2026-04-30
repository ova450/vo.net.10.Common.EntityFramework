using CORM.Core.Domain.Model;

namespace CORM.AppExample.Model
{
    public class OrderItem : IEntity, IEntityNamed, IParents<Order, Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId1 { get; set; }
        public int ParentId2 { get; set; }
        public IEntity<int> ParentEntity1 { get; set; }
        public IEntity<int> ParentEntity2 { get; set; }
    }
}