using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CORM.Core.Infrastructure
{
    public interface IContext : IDisposable, ICurrentDbContext { }

}
