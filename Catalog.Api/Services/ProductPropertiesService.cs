using AutoMapper;
using Products.Api.Dto.RequestDto.ProductProperties;
using Products.Api.Repositories;

namespace Products.Api.Services;

public class ProductPropertiesService
{
    private readonly IMapper _mapper;
    private readonly ProductPropertiesRepository _productPropertiesRepository;
    
    public ProductPropertiesService(ProductPropertiesRepository productPropertiesRepository, IMapper mapper)
    {
        _mapper = mapper;
        _productPropertiesRepository = productPropertiesRepository;
    }
    
    public async Task AddRangeAsync(ProductPropertiesRequest request) => 
        await _productPropertiesRepository.AddRangeAsync(request.ProductProperties);
}