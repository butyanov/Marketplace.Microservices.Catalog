using FluentValidation;
using Products.Api.Dto.RequestDto.ProductProperties;

namespace Products.Api.Validators.ProductProperties;

public class ProductPropertiesRequestValidator : AbstractValidator<ProductPropertiesRequest>
{
    public ProductPropertiesRequestValidator()
    {
        RuleFor(pp => pp.ProductProperties)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmptyField)
            .Must(pp => pp.Count > 0)
            .WithMessage(ValidationMessages.TooShortValue);
    }
}