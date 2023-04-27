namespace Products.Api.Repositories;

public interface ICrudRepository<T> : IRepository<T>
{
    IQueryable<T> GetAll();
    Task<T> GetByIdAsync(Guid id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
}