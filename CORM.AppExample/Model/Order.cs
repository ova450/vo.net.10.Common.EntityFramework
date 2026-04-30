using CORM.Core.Domain.Model;

namespace CORM.AppExample.Model
{
    public class Order(int userId, ICollection<(int productId, int quantity)> items) : IEntityBase, IDoneTiming, IEntityDetails, IParent<User>, IChild<OrderItem>
    {
        public int Id { get; set; }
        public string? Description { get; init; }
        public string? LogoUrl { get; init; }
        public string? WebsiteUrl { get; init; }
        public int ParentId { get; set; }
        public User? Parent { get; set; }
        public ICollection<OrderItem> Children { get; set; } = (ICollection<OrderItem>)items;
        public DateTimeOffset Done { get; init; } = DateTimeOffset.UtcNow;
        public int DoneBy { get; init; }
        public double TotalAmount { get; set; } = 0;
    }
}