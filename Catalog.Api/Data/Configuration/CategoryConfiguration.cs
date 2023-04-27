using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Api.Data.Configuration.Abstractions;
using Products.Api.Models;

namespace Products.Api.Data.Configuration;

public class CategoryConfiguration : BaseConfiguration<Category>
{
    public override void ConfigureChild(EntityTypeBuilder<Category> typeBuilder)
    {
        typeBuilder.HasIndex(c => c.Name).IsUnique();
    }
}