using CORM.Core.Domain.Model;

namespace CORM.AppExample.Model
{
    public class OrderItem : IEntityBase, IParent<Product>
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public Product? Parent { get; set; }
    }
}