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

            RuleFor(d => d.CertificateNumber)
               .NotEmpty()
               .MaximumLength(100)
               .WithMessage("NameOfBussiness is required");

            RuleFor(t => t.CertificateGivenDate)
              .NotNull()
              .WithMessage("Appeal Date date is required.");

            RuleFor(d => d.PreviousAppeal)
                 .NotEmpty()
                 .MaximumLength(100)
                 .WithMessage("NameOfBussiness is required");

            RuleFor(d => d.AppealText)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("NameOfBussiness is required");

            RuleFor(t => t.TotalClaimAmount)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("IncomingCount is required.");

            RuleFor(t => t.MainDebt)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("IncomingCount is required.");

            RuleFor(t => t.CalculatedLateCharges)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("IncomingCount is required.");

            RuleFor(t => t.AmountOfFine)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("IncomingCount is required.");

            RuleFor(t => t.Percentage)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("IncomingCount is required.");

         
            RuleFor(t => t.AppealPredmetId)
                .NotEmpty()
                .NotNull()
                .WithMessage("Application Type id is required.");

            RuleFor(t => t.AppealTypeId)
               .NotEmpty()
               .NotNull()
               .WithMessage("Application Type id is required.");
        }
    }
}
