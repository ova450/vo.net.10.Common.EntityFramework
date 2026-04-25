using CORM.Core.Entities;

namespace CORM.TestConsole.Entities;

public class User : IEntity
{
    public int Id { get; init; }
    public string Name { get; set; } = string.Empty; // Изменено с init на set
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}