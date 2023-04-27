using Products.Api.Models;

namespace Products.Api.Dto.ResponseDto;

public class ProductResponse
{
    public Guid Id { get; set; }
    public Guid? CategoryId { get; set; }
    public string? Name { get; set; }
    public decimal? Price { get; set; }
    public Guid? Images { get; set; }
    public string? Description { get; set; }
    public List<ProductProperty>? Properties { get; set; }
}