using Products.Api.Data;
using Products.Api.Data.Extensions;
using Products.Api.Models;
using Products.Api.Repositories.Extensions;

namespace Products.Api.Repositories;

public class BrandsRepository : ICrudRepository<Brand>
{
    private readonly ProductsDbContext _dbContext;

    public BrandsRepository(ProductsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Brand> GetAll() =>
        _dbContext.Brands.AsQueryable();
    
    public async Task<Brand> GetByIdAsync(Guid id) => 
        await _dbContext.Brands.FirstOrNotFoundAsync(c => c.Id == id);
    
    public async Task AddAsync(Brand entity)
    {
        await _dbContext.Brands.AddAsync(entity);
        await _dbContext.SaveEntitiesAsync();
    }
    
    public async Task UpdateAsync(Brand newEntity)
    {
        var oldEntity = await _dbContext.Brands.FirstOrNotFoundAsync(p => p.Id == newEntity.Id);
        _dbContext.UpdateEntity(oldEntity, newEntity);
        await _dbContext.SaveEntitiesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _dbContext.Brands.FirstOrNotFoundAsync(c => c.Id == id);
        _dbContext.Brands.Remove(entity);
        await _dbContext.SaveEntitiesAsync();
    }
}