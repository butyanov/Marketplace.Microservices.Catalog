using FluentValidation;
using Products.Api.Dto.RequestDto.Category;

namespace Products.Api.Validators.Category;

public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
{
    public UpdateCategoryRequestValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmptyField);
        RuleFor(c => c.Name)
            .MinimumLength(3)
            .WithMessage(ValidationMessages.TooShortValue)
            .MaximumLength(40)
            .WithMessage(ValidationMessages.TooLongValue);
        
    }
}