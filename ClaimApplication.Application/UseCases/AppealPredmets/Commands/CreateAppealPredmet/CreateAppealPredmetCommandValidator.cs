using FluentValidation;

namespace ClaimApplication.Application.UseCases.AppealPredmets.Commands.CreateAppealPredmet
{
    public class CreateAppealPredmetCommandValidator : AbstractValidator<CreateAppealPredmetCommand>
    {
        public CreateAppealPredmetCommandValidator()
        {

            RuleFor(d => d.Name)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Name is required");
        }
    }
}
