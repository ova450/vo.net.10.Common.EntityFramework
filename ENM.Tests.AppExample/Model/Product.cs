using EntityNexus.Abstractions.Domain.Model;

namespace ENM.Tests.AppExample.Model;

public class Product(string name) : AEntityNamed(name)
{
    public decimal Price { get; set; }
    public int Stock { get; set; }
}