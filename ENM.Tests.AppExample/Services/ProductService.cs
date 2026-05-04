using EntityNexus.Abstractions.Domain.Service;
using EntityNexus.Tests.AppExample.Data;
using EntityNexus.Tests.AppExample.Model;

namespace EntityNexus.Tests.AppExample.Services;

public class ProductService(AppDbContext db) : ARepositoryAsync<Product>(db);
