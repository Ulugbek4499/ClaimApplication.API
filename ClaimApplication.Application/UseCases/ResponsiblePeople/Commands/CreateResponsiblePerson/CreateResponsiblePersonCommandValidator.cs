using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                .WithMessage("Name is required");

            RuleFor(d => d.Inn)
              .NotEmpty()
              .MaximumLength(100)
              .WithMessage("Name is required");

            RuleFor(t => t.ResponsiblePersonTypeId)
                .NotEmpty()
                .NotNull()
                .WithMessage("ResponsiblePerson Type id is required.");


            RuleFor(d => d.Description)
                .NotEmpty()
                .MaximumLength(250)
                .WithMessage("Description is required");

            RuleFor(d => d.Barcode)
               .NotEmpty()
               .MaximumLength(250)
               .WithMessage("Barcode is required");

            RuleFor(d => d.MeasureType)
                .IsInEnum()
                .WithMessage("Invalid MeasureType");
        }
    }
}
