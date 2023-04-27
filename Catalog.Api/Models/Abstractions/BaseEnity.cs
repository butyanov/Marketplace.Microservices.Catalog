namespace Products.Api.Models.Abstractions;

public abstract class BaseEntity
{
    public Guid Id { get; protected init; }

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
    }
}