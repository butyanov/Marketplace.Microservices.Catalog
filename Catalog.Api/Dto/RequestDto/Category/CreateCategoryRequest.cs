namespace Products.Api.Dto.RequestDto.Category;

public record CreateCategoryRequest(Guid? ParentId, string Name);