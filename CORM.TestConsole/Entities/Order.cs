using CORM.Core.Entities;

namespace CORM.TestConsole.Entities;

public class Order : IEntityBase
{
    public int Id { get; init; }
    public int UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public User? User { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}