using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Products.Api.Exceptions;

namespace Products.Api.Repositories.Extensions;

public static class InfrastructureQueryableExtensions
{
    public static async Task<List<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageNum, int pageSize, string orderField = "Name") =>
        await source.Paginate(pageNum, pageSize, orderField).ToListAsync();


    public static IQueryable<T> Paginate<T>(this IQueryable<T> source, int pageNum, int pageSize, string orderField = "Name")
    {
        var property = typeof(T).GetProperties().AsQueryable().FirstOrNotFound(pi => pi.Name == orderField);
        return source.OrderBy(p => property.Name).Skip((pageNum - 1) * pageSize).Take(pageSize);
    }
    
    public static async Task<IQueryable<T>> PaginateAsync<T>(this IQueryable<T> source, int pageNum, int pageSize, string orderField = "Name")
    {
        var property = await typeof(T).GetProperties().AsQueryable().FirstOrNotFoundAsync(pi => pi.Name == orderField);
        return source.OrderBy(p => property.Name).Skip((pageNum - 1) * pageSize).Take(pageSize);
    }
    
    public static async Task<T> FirstOrNotFoundAsync<T>(
        this IQueryable<T> queryable,
        CancellationToken cancellationToken = default)
    {
        return await queryable.FirstOrDefaultAsync(cancellationToken) 
               ?? throw new NotFoundException<T>();
    }
    
    public static async Task<T> FirstOrNotFoundAsync<T>(
        this IQueryable<T> queryable,
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await queryable.FirstOrDefaultAsync(predicate, cancellationToken) 
               ?? throw new NotFoundException<T>();
    }
    
    public static T FirstOrNotFound<T>(
        this IQueryable<T> queryable,
        Expression<Func<T, bool>> predicate)
    {
        return queryable.FirstOrDefault(predicate) 
               ?? throw new NotFoundException<T>();
    }
    
    public static async Task<bool> AnyOrNotFoundAsync<T>(
        this IQueryable<T> queryable,
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await queryable.AnyAsync(predicate, cancellationToken) 
            ? true 
            : throw new NotFoundException<T>();
    }
    
    public static bool AnyOrNotFound<T>(
        this IQueryable<T> queryable,
        Expression<Func<T, bool>> predicate)
    {
        return queryable.Any(predicate) 
            ? true 
            : throw new NotFoundException<T>();
    }
}