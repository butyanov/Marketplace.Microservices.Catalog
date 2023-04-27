using Microsoft.AspNetCore.Mvc;
using Products.Api.Authorization;
using Products.Api.Dto.RequestDto;
using Products.Api.Dto.RequestDto.Product;
using Products.Api.Dto.ResponseDto;
using Products.Api.Repositories;
using Products.Api.Services;

namespace Products.Api.Controllers;

[ApiController]
[Route($"catalog/[controller]")]
public class ProductsController : Controller
{
    private readonly ProductsRepository _productsRepository;
    private readonly ProductsService _productsService;
    private readonly ProductsFilterService _productsFilterService;

    public ProductsController(ProductsRepository productsRepository, ProductsService productsService, ProductsFilterService productsFilterService)
    {
        _productsRepository = productsRepository;
        _productsService = productsService;
        _productsFilterService = productsFilterService;
    }

    [HttpGet]
    [Permissions(UserPermissionsPresets.User)]
    public async Task<IEnumerable<ProductResponse>> GetAll([FromQuery] PaginatedRequest paginatedRequest)
    {
        var products = _productsRepository.GetAll();
        return await _productsService.ToPaginatedProductResponsesAsync(products, paginatedRequest);
    }
    
    [HttpGet]
    [Route("{id:guid}")]   
    [Permissions(UserPermissionsPresets.User)]
    public async Task<ProductResponse> Get([FromRoute] Guid id) =>
        await _productsService.GetAsync(id);
    
    [HttpPost]
    [Permissions(UserPermissionsPresets.Moderator)]
    public async Task Create([FromBody] CreateProductRequest request) => 
        await _productsService.AddAsync(request);
    
    [HttpPatch]
    [Permissions(UserPermissionsPresets.Moderator)]
    public async Task Update(UpdateProductRequest request) =>
        await _productsService.UpdateAsync(request);
    
    [HttpDelete]
    [Route("{id:guid}")]
    [Permissions(UserPermissionsPresets.Moderator)]
    public async Task Delete(Guid id) => 
        await _productsRepository.DeleteAsync(id);
    
    [HttpGet]
    [Route("search")]
    [Permissions(UserPermissionsPresets.User)]
    public async Task<IEnumerable<ProductResponse>> Search(string query,[FromQuery] PaginatedRequest paginatedRequest)
    {
        var products = _productsFilterService.SearchInRange(_productsRepository.GetAll(), query);
        return await _productsService.ToPaginatedProductResponsesAsync(products, paginatedRequest);
    }
    
    [HttpGet]
    [Route("category/{categoryId:guid}")]
    [Permissions(UserPermissionsPresets.User)]
    public async Task<IEnumerable<ProductResponse>> GetByCategory([FromRoute] Guid categoryId, [FromQuery] PaginatedRequest paginatedRequest,
        [FromQuery]FilterPropertiesRequest filterPropertiesRequest,
        [FromQuery] PriceRangeRequest priceRangeRequest)
    {
       var products = _productsFilterService.GetRange(_productsRepository.GetAll(), categoryId,
            filterPropertiesRequest.FilterProperties, priceRangeRequest.Floor, priceRangeRequest.Ceiling);
       return await _productsService.ToPaginatedProductResponsesAsync(products, paginatedRequest);
    }
    
    [HttpGet]
    [Route("category/{categoryId:guid}/search")]
    [Permissions(UserPermissionsPresets.User)]
    public async Task<IEnumerable<ProductResponse>> SearchInCategory([FromRoute] Guid categoryId, [FromQuery] PaginatedRequest paginatedRequest,
        [FromQuery] FilterPropertiesRequest filterPropertiesRequest,
        [FromQuery] PriceRangeRequest priceRangeRequest, [FromQuery] string query)
    {
        var products = _productsFilterService.GetRange(_productsRepository.GetAll(), categoryId,
            filterPropertiesRequest.FilterProperties, priceRangeRequest.Floor, priceRangeRequest.Ceiling);
        
        var result = _productsFilterService.SearchInRange(products, query);

        return await _productsService.ToPaginatedProductResponsesAsync(result, paginatedRequest);
    }
}
