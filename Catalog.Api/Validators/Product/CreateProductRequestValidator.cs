using FluentValidation;
using Products.Api.Dto.RequestDto.Product;

namespace Products.Api.Validators.Product;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmptyField)
            .MinimumLength(10)
            .WithMessage(ValidationMessages.TooShortValue)
            .MaximumLength(100)
            .WithMessage(ValidationMessages.TooLongValue);
        
        RuleFor(p => p.Price)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmptyField)
            .Must(p => p > 0)
            .WithMessage(ValidationMessages.TooLowValue);
        
        RuleFor(p => p.Description)
            .MinimumLength(20)
            .WithMessage(ValidationMessages.TooShortValue)
            .MaximumLength(500)
            .WithMessage(ValidationMessages.TooLongValue);
    }
}