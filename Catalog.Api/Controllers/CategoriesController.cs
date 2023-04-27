using Microsoft.AspNetCore.Mvc;
using Products.Api.Authorization;
using Products.Api.Dto.RequestDto;
using Products.Api.Dto.RequestDto.Brand;
using Products.Api.Dto.RequestDto.Category;
using Products.Api.Models;
using Products.Api.Repositories;
using Products.Api.Repositories.Extensions;
using Products.Api.Services;

namespace Products.Api.Controllers;

[ApiController]
[Route($"catalog/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly CategoriesRepository _categoriesRepository;
    private readonly CategoriesService _categoriesService;

    public CategoriesController(CategoriesRepository productsRepository, CategoriesService brandsService)
    {
        _categoriesRepository = productsRepository;
        _categoriesService = brandsService;
    }
    
    [HttpGet]
    [Permissions(UserPermissionsPresets.User)]
    public async Task<IEnumerable<Category>> GetAll([FromQuery] PaginatedRequest paginatedRequest) =>
        await _categoriesRepository.GetAll().ToPaginatedListAsync(paginatedRequest.PageNum, paginatedRequest.PageSize, paginatedRequest.OrderField ?? "Name");
    
    [HttpGet]
    [Route("{id:guid}")]    
    [Permissions(UserPermissionsPresets.User)]
    public async Task<Category> Get([FromRoute] Guid id) =>
        await _categoriesRepository.GetByIdAsync(id);

    [HttpPost]
    [Permissions(UserPermissionsPresets.Moderator)]
    public async Task Create([FromBody] CreateCategoryRequest request) => 
        await _categoriesService.AddAsync(request);
    
    [HttpPatch]
    [Permissions(UserPermissionsPresets.Moderator)]
    public async Task Update([FromBody] UpdateCategoryRequest request) =>
        await _categoriesService.UpdateAsync(request);
    
    [HttpDelete]
    [Route("{id:guid}")]
    [Permissions(UserPermissionsPresets.Moderator)]
    public async Task Delete([FromRoute] Guid id) => 
        await _categoriesRepository.DeleteAsync(id);
}