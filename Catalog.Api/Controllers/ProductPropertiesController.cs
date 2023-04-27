using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products.Api.Authorization;
using Products.Api.Dto.RequestDto;
using Products.Api.Dto.RequestDto.ProductProperties;
using Products.Api.Models;
using Products.Api.Repositories;
using Products.Api.Repositories.Extensions;
using Products.Api.Services;

namespace Products.Api.Controllers;

[ApiController]
[Route($"catalog/[controller]")]
public class ProductPropertiesController : ControllerBase
{
    private readonly ProductPropertiesRepository _productPropertiesRepository;
    private readonly ProductPropertiesService _productPropertiesService;

    public ProductPropertiesController(ProductPropertiesRepository productPropertiesRepository, ProductPropertiesService productPropertiesService)
    {
        _productPropertiesRepository = productPropertiesRepository;
        _productPropertiesService = productPropertiesService;
    }
    
    [HttpGet]
    [Permissions(UserPermissionsPresets.User)]
    public async Task<IEnumerable<ProductProperty>> GetAll([FromQuery] PaginatedRequest paginatedRequest) =>
        await _productPropertiesRepository.GetAll()
            .ToPaginatedListAsync(paginatedRequest.PageNum, paginatedRequest.PageSize, paginatedRequest.OrderField ?? "Type");
    
    [HttpGet]
    [Route("{id:guid}")] 
    [Permissions(UserPermissionsPresets.User)]
    public async Task<IEnumerable<ProductProperty>> Get([FromRoute] Guid id) =>
        await _productPropertiesRepository.GetById(id).ToListAsync();

    [HttpPost]
    [Permissions(UserPermissionsPresets.Moderator)]
    public async Task Create([FromBody] ProductPropertiesRequest request) => 
        await _productPropertiesService.AddRangeAsync(request);
    
    [HttpDelete]
    [Route("{productId:guid}")]
    [Permissions(UserPermissionsPresets.Moderator)]
    public async Task Delete([FromRoute] Guid productId) => 
        await _productPropertiesRepository.DeleteRangeAsync(productId);
    
    [HttpDelete]
    [Route("{productId:guid}/{type}")]
    [Permissions(UserPermissionsPresets.Admin)]
    public async Task Delete([FromRoute] Guid productId,[FromQuery] string type) => 
        await _productPropertiesRepository.DeleteAsync(productId, type);
}