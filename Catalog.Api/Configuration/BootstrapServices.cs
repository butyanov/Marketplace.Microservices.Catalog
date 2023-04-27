
using Products.Api.Services;

namespace Products.Api.Configuration;

public static class BootstrapServices
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Program).Assembly);
        services.AddScoped<CategoriesService>();
        services.AddScoped<BrandsService>();
        services.AddScoped<ProductPropertiesService>();
        services.AddScoped<ProductsFilterService>();
        services.AddScoped<ProductsService>();
        
        return services;
    }
}