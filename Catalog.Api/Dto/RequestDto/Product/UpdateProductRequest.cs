namespace Products.Api.Dto.RequestDto.Product;

public class UpdateProductRequest
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public Guid BrandId { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
}