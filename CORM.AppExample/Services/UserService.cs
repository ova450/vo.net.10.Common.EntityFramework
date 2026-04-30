using CORM.AppExample.Data;
using CORM.AppExample.Model;
using CORM.Core.Abstraction.Domain.Service;

namespace CORM.AppExample.Services
{
    public class UserService(AppDbContext db) : Repository<User>(db) { }
}
