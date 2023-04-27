using FluentValidation;
using Products.Api.Dto.RequestDto.Brand;

namespace Products.Api.Validators.Brand;

public class CreateBrandRequestValidator : AbstractValidator<CreateBrandRequest>
{
    public CreateBrandRequestValidator()
    {
        RuleFor(b => b.Name)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmptyField)
            .MinimumLength(3)
            .WithMessage(ValidationMessages.TooShortValue)
            .MaximumLength(20)
            .WithMessage(ValidationMessages.TooLongValue);
        
        RuleFor(b => b.Description)
            .MinimumLength(10)
            .WithMessage(ValidationMessages.TooShortValue)
            .MaximumLength(200)
            .WithMessage(ValidationMessages.TooLongValue);
    }
}