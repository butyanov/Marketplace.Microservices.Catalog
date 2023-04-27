namespace Products.Api.Dto.RequestDto.Product;

public record FilterPropertiesRequest(Dictionary<string, List<string>>? FilterProperties);
