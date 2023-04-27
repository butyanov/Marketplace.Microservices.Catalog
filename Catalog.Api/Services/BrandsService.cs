using AutoMapper;
using Products.Api.Dto.RequestDto.Brand;
using Products.Api.Models;
using Products.Api.Repositories;

namespace Products.Api.Services;

public class BrandsService
{
    private readonly IMapper _mapper;
    private readonly BrandsRepository _brandsRepository;
    
    public BrandsService(BrandsRepository brandsRepository, IMapper mapper)
    {
        _brandsRepository = brandsRepository;
        _mapper = mapper;
    }
    
    public async Task AddAsync(CreateBrandRequest request) => 
        await _brandsRepository.AddAsync(_mapper.Map<CreateBrandRequest, Brand>(request));
    
    public async Task UpdateAsync(UpdateBrandRequest request) =>
        await _brandsRepository.UpdateAsync(_mapper.Map<UpdateBrandRequest, Brand>(request));
    
}