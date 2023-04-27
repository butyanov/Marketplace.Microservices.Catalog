using AutoMapper;
using Products.Api.Dto.RequestDto.Category;
using Products.Api.Models;
using Products.Api.Repositories;

namespace Products.Api.Services;

public class CategoriesService
{
    private readonly IMapper _mapper;
    private readonly CategoriesRepository _categoriesRepository;
    
    public CategoriesService(CategoriesRepository categoriesRepository, IMapper mapper)
    {
        _categoriesRepository = categoriesRepository;
        _mapper = mapper;
    }
    
    public async Task AddAsync(CreateCategoryRequest request) => 
        await _categoriesRepository.AddAsync(_mapper.Map<CreateCategoryRequest, Category>(request));
    
    public async Task UpdateAsync(UpdateCategoryRequest request) =>
        await _categoriesRepository.UpdateAsync(_mapper.Map<UpdateCategoryRequest, Category>(request));
}