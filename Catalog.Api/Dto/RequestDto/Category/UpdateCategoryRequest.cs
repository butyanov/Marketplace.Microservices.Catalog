namespace Products.Api.Dto.RequestDto.Category;

public record UpdateCategoryRequest(Guid Id, Guid? ParentId, string Name);