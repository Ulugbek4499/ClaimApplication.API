using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimApplication.Application.UseCases.ResponsiblePeople.Commands.CreateApplication;
using FluentValidation;

namespace ClaimApplication.Application.UseCases.Applications.Commands.CreateApplication
{
    public class CreateApplicationCommandValidator : AbstractValidator<CreateApplicationCommand>
    {
        public CreateApplicationCommandValidator()
        {
            RuleFor(d => d.Inn)
              .NotEmpty()
              .MaximumLength(100)
              .WithMessage("Inn is required");

            RuleFor(d => d.OrdinalNumber)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Ordinal Number is required");

            RuleFor(d => d.NameOfBussiness)
                 .NotEmpty()
                 .MaximumLength(100)
                 .WithMessage("NameOfBussiness is required");

            RuleFor(t => t.AppealNumber)
                 .NotNull()
                 .GreaterThan(0)
                 .WithMessage("IncomingCount is required.");

            RuleFor(t => t.AppealDate)
                .NotNull()
                .WithMessage("Appeal Date date is required.");

            RuleFor(d => d.MembershipAgreementNumber)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("NameOfBussiness is required");

           RuleFor(t => t.MembershipAgreementDate)
                .NotNull()
                .WithMessage("Appeal Date date is required.");

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
                .WithMessage("Application Type id is required.");

            RuleFor(t => t.TypeOfApplicationId)
               .NotEmpty()
               .NotNull()
               .WithMessage("Application Type id is required.");
        }
    }
}
