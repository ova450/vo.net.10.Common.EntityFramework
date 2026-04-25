using CORM.Core.Entities;

namespace CORM.TestConsole.Entities;

public class OrderItem : IEntityBase
{
    public int Id { get; init; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    
    // Navigation properties
    public Order? Order { get; set; }
    public Product? Product { get; set; }
}