using AutoMapper;
using Products.Api.Dto.RequestDto;
using Products.Api.Dto.RequestDto.Product;
using Products.Api.Dto.ResponseDto;
using Products.Api.Models;
using Products.Api.Repositories;
using Products.Api.Repositories.Extensions;

namespace Products.Api.Services;

public class ProductsService 
{
    private readonly ProductsRepository _productsRepository;
    private readonly ProductPropertiesRepository _propertiesRepository;
    private readonly IMapper _mapper;

    public ProductsService(ProductsRepository productsRepository, ProductPropertiesRepository propertiesRepository, IMapper mapper)
    {
        _productsRepository = productsRepository;
        _propertiesRepository = propertiesRepository;
        _mapper = mapper;
    }
    
    public async Task<ProductResponse> GetAsync(Guid id)
    {
        var product = await _productsRepository.GetByIdAsync(id);
        var properties = _propertiesRepository.GetById(id);
        
        var response = _mapper.Map<Product, ProductResponse>(product);
        response.Properties = properties.ToList();
        
        return response;
    }

    public async Task AddAsync(CreateProductRequest request)
    {
        var product = _mapper.Map<CreateProductRequest, Product>(request);
        await _productsRepository.AddAsync(product);
    }
    
    public async Task UpdateAsync(UpdateProductRequest request)
    {
        var product = _mapper.Map<UpdateProductRequest, Product>(request);
        await _productsRepository.UpdateAsync(product);
    }
    
    public async Task<IEnumerable<ProductResponse>> ToPaginatedProductResponsesAsync(IQueryable<Product> products, PaginatedRequest paginatedRequest)
    {
        return AggregateProducts(await products
            .ToPaginatedListAsync(paginatedRequest.PageNum, paginatedRequest.PageSize, paginatedRequest.OrderField ?? "Name"));
    }

    private IEnumerable<ProductResponse> AggregateProducts(List<Product> products)
    {
        var response = new List<ProductResponse>(products.Count);
        response.AddRange(products.Select(t => _mapper.Map<Product, ProductResponse>(t)));
        response.ForEach(p => p.Properties = _propertiesRepository.GetAll().Where(pp => pp.ProductId == p.Id).ToList());
        
        return response;
    }
}