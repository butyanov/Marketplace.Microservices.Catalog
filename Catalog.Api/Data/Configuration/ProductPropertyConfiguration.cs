using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Api.Data.Configuration.Abstractions;
using Products.Api.Models;

namespace Products.Api.Data.Configuration;

public class ProductPropertyConfiguration : DependencyInjectedEntityConfiguration<ProductProperty>
{
    public override void Configure(EntityTypeBuilder<ProductProperty> builder)
    {
        builder.HasKey(pp => new { pp.ProductId, pp.Type });
        
        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(p => p.ProductId);
    }
}