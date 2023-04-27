using Microsoft.EntityFrameworkCore;
using Products.Api.Data;
using Products.Api.Data.Extensions;
using Products.Api.Models;
using Products.Api.Repositories.Extensions;

namespace Products.Api.Repositories;

public class CategoriesRepository : ICrudRepository<Category>
{
    private readonly ProductsDbContext _dbContext;

    public CategoriesRepository(ProductsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Category> GetAll() =>
        _dbContext.Categories.AsQueryable();
    
    public async Task<Category> GetByIdAsync(Guid id) => 
        await _dbContext.Categories.FirstOrNotFoundAsync(c => c.Id == id);
    
    public async Task AddAsync(Category entity)
    {
        if (entity.ParentId != null)
            await _dbContext.Categories.AnyOrNotFoundAsync(c => c.Id == entity.ParentId);
        await _dbContext.Categories.AddAsync(entity);
        await _dbContext.SaveEntitiesAsync();
    }
    
    public async Task UpdateAsync(Category newEntity)
    {
        if (newEntity.ParentId != null)
            await _dbContext.Categories.AnyOrNotFoundAsync(c => c.Id == newEntity.ParentId);
        var oldEntity = await _dbContext.Categories.FirstOrNotFoundAsync(p => p.Id == newEntity.Id);
        _dbContext.UpdateEntity(oldEntity, newEntity);
        await _dbContext.SaveEntitiesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _dbContext.Categories.FirstOrNotFoundAsync(c => c.Id == id);
        var childEntities = _dbContext.Categories.Where(c => c.ParentId == entity.Id);
        
        foreach (var c in childEntities)
            c.ParentId = null;
        
        _dbContext.UpdateRange(childEntities);
        _dbContext.Categories.Remove(entity);
        await _dbContext.SaveEntitiesAsync();
    }
    
}