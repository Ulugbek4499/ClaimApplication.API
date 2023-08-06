using FluentValidation;

namespace ClaimApplication.Application.UseCases.TypeOfResponsiblePeople.Commands.UpdateTypeOfResponsiblePerson
{
    public class UpdateTypeOfResponsiblePersonCommandValidator : AbstractValidator<UpdateTypeOfResponsiblePersonCommand>
    {
        public UpdateTypeOfResponsiblePersonCommandValidator()
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
