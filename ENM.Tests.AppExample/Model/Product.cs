using EntityNexus.Abstractions.Domain.Model;

namespace EntityNexus.Tests.AppExample.Model;

public class Product(string name) : AEntityNamed
{
    public decimal Price { get; set; }
    public int Stock { get; set; }
}