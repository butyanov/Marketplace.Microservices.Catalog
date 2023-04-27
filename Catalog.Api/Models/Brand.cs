using Products.Api.Models.Abstractions;

namespace Products.Api.Models;

public class Brand : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
}