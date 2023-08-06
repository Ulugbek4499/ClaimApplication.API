using FluentValidation;

namespace ClaimApplication.Application.UseCases.TypeOfResponsiblePeople.Commands.CreateTypeOfResponsiblePerson
{
    public class CreateTypeOfResponsiblePersonCommandValidator : AbstractValidator<CreateTypeOfResponsiblePersonCommand>
    {
        public CreateTypeOfResponsiblePersonCommandValidator()
        {
            RuleFor(d => d.Name)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Name is required");
        }
    }
}
