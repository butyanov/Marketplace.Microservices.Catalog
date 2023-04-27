namespace Products.Api.Dto.RequestDto.Brand;

public record UpdateBrandRequest(Guid Id, string? Name, string? Description);