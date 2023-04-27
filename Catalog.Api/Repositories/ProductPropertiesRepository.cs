using Products.Api.Data;
using Products.Api.Models;
using Products.Api.Repositories.Extensions;

namespace Products.Api.Repositories;

public class ProductPropertiesRepository : IRepository<ProductProperty>
{
    private readonly ProductsDbContext _dbContext;

    public ProductPropertiesRepository(ProductsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public IQueryable<ProductProperty> GetAll() =>
        _dbContext.ProductProperties.AsQueryable();
    
    public IQueryable<ProductProperty> GetById(Guid productId) =>
        _dbContext.ProductProperties.Where(pp => pp.ProductId == productId).AsQueryable();
    
    public async Task<ProductProperty> GetAsync(Guid productId, string type) =>
        await _dbContext.ProductProperties.FirstOrNotFoundAsync(pp => pp.ProductId == productId && pp.Type == type);

    public async Task AddRangeAsync(IEnumerable<ProductProperty> productProperties)
    {
        foreach (var pp in productProperties)
            await _dbContext.Products.AnyOrNotFoundAsync(p => p.Id == pp.ProductId);
        
        await _dbContext.ProductProperties.AddRangeAsync(productProperties);
        await _dbContext.SaveEntitiesAsync();
    }
    
    public async Task DeleteAsync(Guid productId, string type)
    {
        var entity = await _dbContext.ProductProperties.FirstOrNotFoundAsync(c => c.ProductId == productId && c.Type == type);
        
        _dbContext.ProductProperties.Remove(entity);
        await _dbContext.SaveEntitiesAsync();
    }
    
    public async Task DeleteRangeAsync(Guid productId)
    {
        await _dbContext.ProductProperties.AnyOrNotFoundAsync(c => c.ProductId == productId);
        
        _dbContext.ProductProperties.RemoveRange(_dbContext.ProductProperties.Where(c => c.ProductId == productId));
        await _dbContext.SaveEntitiesAsync();
    }
}