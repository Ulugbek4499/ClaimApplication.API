using FluentValidation;

namespace ClaimApplication.Application.UseCases.ResponsiblePeople.Commands.CreateResponsiblePerson
{
    public class CreateResponsiblePersonCommandValidator : AbstractValidator<CreateResponsiblePersonCommand>
    {
        public CreateResponsiblePersonCommandValidator()
        {
            RuleFor(d => d.OrdinalNumber)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Ordinal Number is required");

            RuleFor(d => d.Inn)
              .NotEmpty()
              .MaximumLength(100)
              .WithMessage("Inn is required");

            RuleFor(d => d.FullName)
                 .NotEmpty()
                 .MaximumLength(100)
                 .WithMessage("FullName is required");

            RuleFor(d => d.Address)
               .NotEmpty()
               .MaximumLength(100)
               .WithMessage("FullName is required");

            RuleFor(d => d.PhoneNumber)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("FullName is required");

            RuleFor(t => t.ApplicationId)
                .NotEmpty()
                .NotNull()
                .WithMessage("ResponsiblePerson Type id is required.");

            RuleFor(t => t.TypeOfResponsiblePersonId)
               .NotEmpty()
               .NotNull()
               .WithMessage("ResponsiblePerson Type id is required.");
        }
    }
}
