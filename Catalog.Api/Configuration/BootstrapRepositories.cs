using Products.Api.Models;
using Products.Api.Repositories;

namespace Products.Api.Configuration;

public static class BootstrapRepositories
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<CategoriesRepository>();
        services.AddScoped<BrandsRepository>();
        services.AddScoped<ProductPropertiesRepository >();
        services.AddScoped<ProductsRepository>();

        return services;
    }
}