using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Products.Api.Models;
using Products.Api.Repositories;

namespace Products.Api.Services;

public class ProductsFilterService 
{
    private readonly ProductsRepository _productsRepository;
    private readonly ProductPropertiesRepository _propertiesRepository;
    private readonly CategoriesRepository _categoriesRepository;
    private readonly BrandsRepository _brandsRepository;

    public ProductsFilterService(ProductsRepository productsRepository, ProductPropertiesRepository propertiesRepository, IMapper mapper, CategoriesRepository categoriesRepository, BrandsRepository brandsRepository)
    {
        
        _productsRepository = productsRepository;
        _propertiesRepository = propertiesRepository;
        _categoriesRepository = categoriesRepository;
        _brandsRepository = brandsRepository;
    }

    
       public IQueryable<Product> GetRange(IQueryable<Product> products, Guid categoryId, Dictionary<string, List<string>>? filterProperties, decimal? floorPrice, decimal? ceilingPrice)
    {
        if (categoryId != Guid.Empty)
            products = GetRangeByCategory(products, categoryId);
        
        if (filterProperties != null && filterProperties.Any())
            products = GetRangeByProperties(products, filterProperties);
        
        if (floorPrice != null && ceilingPrice != null)
            products = GetRangeByPrice(products, (decimal)floorPrice, (decimal)ceilingPrice);

        return products;
    }
    
    public IQueryable<Product> SearchInRange(IQueryable<Product> range, string query)
    {
        var searchByProperties = _propertiesRepository.GetAll()
            .Where(pp =>  EF.Functions.Like(pp.Value.ToUpper(), $"%{query.ToUpper()}%")).Select(pp => pp.ProductId);
        var searchByCategory = _categoriesRepository.GetAll()
            .Where(c => EF.Functions.Like(c.Name.ToUpper(), $"%{query.ToUpper()}%")).Select(c => c.Id);
        var searchByBrands = _brandsRepository.GetAll()
            .Where(c => EF.Functions.Like(c.Name.ToUpper(), $"%{query.ToUpper()}%")).Select(c => c.Id);
        var products = range
            .Where(p =>  EF.Functions.Like(p.Name.ToUpper(), $"%{query.ToUpper()}%") 
                         || searchByProperties.Contains(p.Id) 
                         || searchByCategory.Contains(p.CategoryId) 
                         || searchByBrands.Contains(p.BrandId));
        
        return products;
    }
    
    public IQueryable<Product> GetRangeByProperties(IQueryable<Product> products, Dictionary<string, List<string>> filterProperties)
    {
        var query = _productsRepository.GetAll();
        foreach (var (type, values) in filterProperties)
        {
            var innerQuery = _propertiesRepository.GetAll()
                .Where(p => p.Type == type && values.Contains(p.Value))
                .Select(p => p.ProductId);
            query = query.Join(innerQuery, p => p.Id, pid => pid, (p, pid) => p);
        }
        return query.Distinct();
    }
    
    public IQueryable<Product> GetRangeByCategory(IQueryable<Product> products, Guid categoryId) => 
        products.Where(p => p.CategoryId == categoryId);

    public IQueryable<Product> GetRangeByPrice(IQueryable<Product> products, decimal floor, decimal ceiling) =>
        products.Where(p => p.Price >= floor && p.Price <= ceiling);

}