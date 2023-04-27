using FluentValidation;
using FluentValidation.AspNetCore;

namespace Products.Api.Configuration;

public static class BoostrapValidation
{
    public static IServiceCollection AddCustomValidation(this IServiceCollection services) =>
        services
            .AddFluentValidationAutoValidation().AddValidatorsFromAssembly(typeof(Program).Assembly);
}