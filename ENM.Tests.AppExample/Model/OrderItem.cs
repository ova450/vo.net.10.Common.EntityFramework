using EntityNexus.Abstractions.Domain.Model;
using EntityNexus.DomainModel;

namespace EntityNexus.Tests.AppExample.Model
{
    public class OrderItem(string name) : AEntityNamed //, IHasParent<Order>
        {
        public int ParentId { get; set; }
        public Order? Parent { get; set; }
    }
}