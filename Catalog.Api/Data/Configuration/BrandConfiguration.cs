using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Api.Data.Configuration.Abstractions;
using Products.Api.Models;

namespace Products.Api.Data.Configuration;

public class BrandConfiguration : BaseConfiguration<Brand>
{
    public override void ConfigureChild(EntityTypeBuilder<Brand> typeBuilder)
    {
        typeBuilder.HasIndex(c =>  c.Name).IsUnique();
    }
}