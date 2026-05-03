using CORM.Core.Abstraction.Domain.Service;
using ENM.Tests.AppExample.Data;
using ENM.Tests.AppExample.Model;

namespace EN.ConventMapDSL.AppExample.Services
{
    public class UserService(AppDbContext db) : Repository<User>(db) { }
}
