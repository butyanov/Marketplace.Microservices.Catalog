namespace Products.Api.Dto.RequestDto;

public class PaginatedRequest
{
    public int PageNum { get; set; }
    public int PageSize { get; set; }
    public string? OrderField { get; set; }
}