using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Api.Models.Abstractions;

namespace Products.Api.Data.Configuration.Abstractions;

public abstract class BaseConfiguration<TEntity> : DependencyInjectedEntityConfiguration<TEntity>
    where TEntity : BaseEntity
{
    public sealed override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        ConfigureBase(builder);
        ConfigureChild(builder);
    }
    
    public abstract void ConfigureChild(EntityTypeBuilder<TEntity> typeBuilder);

    public static void ConfigureBase(EntityTypeBuilder<TEntity> typeBuilder)
    {
        typeBuilder.HasKey(x => x.Id);

        typeBuilder.Property(x => x.Id)
            .HasDefaultValueSql("gen_random_uuid()");
    }
      
}