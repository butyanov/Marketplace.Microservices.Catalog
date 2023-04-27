using FluentValidation;
using Products.Api.Dto.RequestDto;

namespace Products.Api.Validators;

public class PaginatedRequestValidator : AbstractValidator<PaginatedRequest>
{
    public PaginatedRequestValidator()
    {
        RuleFor(pr => pr.PageNum)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmptyField)
            .Must(pn => pn > 0)
            .WithMessage(ValidationMessages.TooLowValue);
        RuleFor(pr => pr.PageSize)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmptyField)
            .Must(ps => ps > 0)
            .WithMessage(ValidationMessages.TooLowValue);

    }
}