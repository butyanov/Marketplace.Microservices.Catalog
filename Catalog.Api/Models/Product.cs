using Products.Api.Models.Abstractions;

namespace Products.Api.Models;

public class Product : BaseEntity
{
    public Guid CategoryId { get; set; }
    public Guid BrandId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public Guid? Images { get; set; }
    public string? Description { get; set; }
}