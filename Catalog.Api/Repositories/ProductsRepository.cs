using Products.Api.Data;
using Products.Api.Data.Extensions;
using Products.Api.Models;
using Products.Api.Repositories.Extensions;

namespace Products.Api.Repositories;

public class ProductsRepository : ICrudRepository<Product>
{
    private readonly ProductsDbContext _dbContext;

    public ProductsRepository(ProductsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Product> GetAll() =>
        _dbContext.Products.AsQueryable();
    
    public async Task<Product> GetByIdAsync(Guid id) =>
        await _dbContext.Products.FirstOrNotFoundAsync(c => c.Id == id);

    public async Task AddAsync(Product entity)
    {
        await _dbContext.Products.AddAsync(entity);
        await _dbContext.SaveEntitiesAsync();
    }
    
    public async Task UpdateAsync(Product newEntity)
    {
        var oldEntity = await _dbContext.Products.FirstOrNotFoundAsync(p => p.Id == newEntity.Id);
        _dbContext.UpdateEntity(oldEntity, newEntity);
        await _dbContext.SaveEntitiesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _dbContext.Products.FirstOrNotFoundAsync(c => c.Id == id);
        _dbContext.Products.Remove(entity);
        await _dbContext.SaveEntitiesAsync();
    }
}