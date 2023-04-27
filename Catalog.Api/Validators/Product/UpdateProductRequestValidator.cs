using System.Data;
using FluentValidation;
using Products.Api.Dto.RequestDto.Product;

namespace Products.Api.Validators.Product;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(p => p.Name)
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