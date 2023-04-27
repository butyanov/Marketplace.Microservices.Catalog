using Microsoft.AspNetCore.Mvc;
using Products.Api.Authorization;
using Products.Api.Dto.RequestDto;
using Products.Api.Dto.RequestDto.Brand;
using Products.Api.Models;
using Products.Api.Repositories;
using Products.Api.Repositories.Extensions;
using Products.Api.Services;

namespace Products.Api.Controllers;

[ApiController]
[Route($"api/catalog/[controller]")]
public class BrandsController : ControllerBase
{
    private readonly BrandsRepository _brandsRepository;
    private readonly BrandsService _brandsService;

    public BrandsController(BrandsRepository productsRepository, BrandsService brandsService)
    {
        _brandsRepository = productsRepository;
        _brandsService = brandsService;
    }
    
    [HttpGet]
    [Permissions(UserPermissionsPresets.User)]
    public async Task<IEnumerable<Brand>> GetAll([FromQuery] PaginatedRequest paginatedRequest) =>
       await _brandsRepository.GetAll().ToPaginatedListAsync(paginatedRequest.PageNum, paginatedRequest.PageSize, paginatedRequest.OrderField ?? "Name");
    
    [HttpGet]
    [Route("{id:guid}")]    
    [Permissions(UserPermissionsPresets.User)]
    public async Task<Brand> Get([FromRoute] Guid id) =>
        await _brandsRepository.GetByIdAsync(id);

    [HttpPost]
    [Permissions(UserPermissionsPresets.Moderator)]
    public async Task Create([FromBody] CreateBrandRequest request) => 
        await _brandsService.AddAsync(request);
    
    [HttpPatch]
    [Permissions(UserPermissionsPresets.Moderator)]
    public async Task Update([FromBody] UpdateBrandRequest request) =>
        await _brandsService.UpdateAsync(request);
    
    [HttpDelete]
    [Route("{id:guid}")]
    [Permissions(UserPermissionsPresets.Moderator)]
    public async Task Delete([FromRoute] Guid id) => 
        await _brandsRepository.DeleteAsync(id);
}
