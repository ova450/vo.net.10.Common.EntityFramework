using CORM.Core.Domain.Model;

namespace CORM.AppExample.Model
{
    public class Product : IEntity,IEntityBase, IEntityTiming
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public IDoneTiming Created { get; init; }
        public ICollection<IDoneTiming> Modified { get; init; }
    }
}