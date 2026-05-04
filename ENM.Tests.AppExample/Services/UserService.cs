using EntityNexus.Abstractions.Domain.Service;
using EntityNexus.Tests.AppExample.Data;
using EntityNexus.Tests.AppExample.Model;

namespace EntityNexus.Tests.AppExample.Services;

public class UserService(AppDbContext db) : ARepositoryAsync<User>(db);
