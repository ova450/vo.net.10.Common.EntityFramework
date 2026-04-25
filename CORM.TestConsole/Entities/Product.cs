using CORM.Core.Entities;

namespace CORM.TestConsole.Entities;

public class Product : IEntity
{
    public int Id { get; init; }
    public string Name { get; set; } = string.Empty; // Изменено с init на set
    public decimal Price { get; set; }
    public int Stock { get; set; }
}