using Products.Api.Models.Abstractions;

namespace Products.Api.Models;

public class Category : BaseEntity
{
    public Guid? ParentId { get; set; }
    public string Name { get; set; }
}