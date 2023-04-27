using FluentValidation;
using Products.Api.Dto.RequestDto.Category;

namespace Products.Api.Validators.Category;

public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryRequestValidator()
    {
        RuleFor(c => c.Name)
            .MinimumLength(3)
            .WithMessage(ValidationMessages.TooShortValue)
            .MaximumLength(40)
            .WithMessage(ValidationMessages.TooLongValue);
    }
}