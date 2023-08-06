using FluentValidation;

namespace ClaimApplication.Application.UseCases.AppealTypes.Commands.UpdateAppealType
{
    public class UpdateAppealTypeCommandValidator : AbstractValidator<UpdateAppealTypeCommand>
    {
        public UpdateAppealTypeCommandValidator()
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
