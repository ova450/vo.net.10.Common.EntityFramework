using CORM.Core.Domain.Model;

namespace CORM.AppExample.Model
{
    public class User : IEntityBase, IDoneTiming
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Done { get; init; } // Created
        public int DoneBy { get; init; } // CreatedBy
    }
}
