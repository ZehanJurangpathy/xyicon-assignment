using FluentValidation;

namespace FlexibleData.Application.Features.FlexibleData.Commands.CreateFlexibleData
{
    public class CreateFlexibleDataCommandValidator : AbstractValidator<CreateFlexibleDataCommand>
    {
        public CreateFlexibleDataCommandValidator()
        {
            RuleFor(c => c.Data).NotEmpty().WithMessage("Data cannot be empty");
        }
    }
}
