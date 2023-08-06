using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimApplication.Application.UseCases.ResponsiblePeople.Commands.UpdateResponsiblePerson;
using FluentValidation;

namespace ClaimApplication.Application.UseCases.ResponsiblePeople.Commands.UpdateResponsiblePerson
{
    public class UpdateResponsiblePersonCommandValidator : AbstractValidator<UpdateResponsiblePersonCommand>
    {
        public UpdateResponsiblePersonCommandValidator()
        {
            RuleFor(t => t.Id)
              .NotEmpty()
              .NotNull()
              .WithMessage("Id is required.");

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
               .WithMessage("Type Of Responsible Person Id is required.");
        }
    }
}
