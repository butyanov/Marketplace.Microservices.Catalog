using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Api.Data.Configuration.Abstractions;
using Products.Api.Models;

namespace Products.Api.Data.Configuration;

public class ProductConfiguration : BaseConfiguration<Product>
{

    public override void ConfigureChild(EntityTypeBuilder<Product> typeBuilder)
    {
        typeBuilder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(p => p.CategoryId);
        typeBuilder.HasOne<Brand>()
            .WithMany()
            .HasForeignKey(p => p.BrandId);
    }
}