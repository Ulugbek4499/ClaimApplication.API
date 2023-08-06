using FluentValidation;

namespace ClaimApplication.Application.UseCases.AppealTypes.Commands.CreateAppealType
{
    public class CreateAppealTypeCommandValidator : AbstractValidator<CreateAppealTypeCommand>
    {
        public CreateAppealTypeCommandValidator()
        {
            RuleFor(d => d.Name)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Name is required");
        }
    }
}
