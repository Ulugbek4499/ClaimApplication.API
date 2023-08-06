using FluentValidation;

namespace ClaimApplication.Application.UseCases.AppealPredmets.Commands.UpdateAppealPredmet
{
    public class UpdateAppealPredmetCommandValidator : AbstractValidator<UpdateAppealPredmetCommand>
    {
        public UpdateAppealPredmetCommandValidator()
        {
            RuleFor(t => t.Id)
               .NotEmpty()
               .NotNull()
               .WithMessage("Id is required.");

            RuleFor(d => d.Name)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Name is required");
        }
    }
}
