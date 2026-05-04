using EntityNexus.Abstractions.Domain.Model;
using EntityNexus.DomainModel;

namespace EntityNexus.Tests.AppExample.Model;

public class Order : AEntity, IHasParent<User>, IHasChild<OrderItem>
{
    public int ParentId { get; set; }
    public User? Parent { get; set; }
    public ICollection<OrderItem> Children { get; set; } = [];

    public decimal TotalAmount { get; set; }
}

