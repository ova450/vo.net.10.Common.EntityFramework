using EntityNexus.Abstractions.Domain.Model;
using EntityNexus.Additionals;
using EntityNexus.DomainModel;

namespace ENM.Tests.AppExample.Model
{
    public class Order(int userId, ICollection<(int productId, int quantity)> items) : AEntity, IDetails, IHasParent<User>
    {
        //public string? Description { get; init; }
        //public string? LogoUrl { get; init; }
        //public string? WebsiteUrl { get; init; }
        //public int ParentId { get; set; }
        //public User? Parent { get; set; }
        //public ICollection<OrderItem> Children { get; set; } = (ICollection<OrderItem>)items;
        //public DateTimeOffset Done { get; init; } = DateTimeOffset.UtcNow;
        //public int DoneBy { get; init; }
        //public double TotalAmount { get; set; } = 0;
        public string? Description { get; init; }
        public string? LogoUrl { get; init; }
        public string? WebsiteUrl { get; init; }
    }
}