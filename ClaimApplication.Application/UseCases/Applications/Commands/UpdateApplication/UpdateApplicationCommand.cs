using AutoMapper;
using ClaimApplication.Application.Commons.Exceptions;
using ClaimApplication.Application.Commons.Interfaces;
using MediatR;

namespace ClaimApplication.Application.UseCases.Applications.Commands.UpdateApplication
{
    public class UpdateApplicationCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Inn { get; set; }
        public string NameOfBussiness { get; set; }
        public int AppealNumber { get; set; }
        public DateTime AppealDate { get; set; }
        public string MembershipAgreementNumber { get; set; }
        public DateTime MembershipAgreementDate { get; set; }
        public string CertificateNumber { get; set; } = null!;
        public DateTime CertificateGivenDate { get; set; }
        public string? PreviousAppeal { get; set; }
        public string AppealText { get; set; }
        public decimal? TotalClaimAmount { get; set; }
        public decimal? MainDebt { get; set; }
        public decimal? CalculatedLateCharges { get; set; }
        public decimal? AmountOfFine { get; set; }
        public decimal? Percentage { get; set; }

        public Guid AppealPredmetId { get; set; }
        public Guid AppealTypeId { get; set; }
    }
    public class UpdateApplicationCommandHandler : IRequestHandler<UpdateApplicationCommand>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public UpdateApplicationCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task Handle(UpdateApplicationCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Application? application = await _context.Applications.FindAsync(request.Id);
            _mapper.Map(request, application);

            if (application is null)
                throw new NotFoundException(nameof(application), request.Id);

            var appealPredmet = await _context.AppealPredmets.FindAsync(request.AppealPredmetId);

            if (appealPredmet is null)
                throw new NotFoundException(nameof(appealPredmet), request.AppealPredmetId);

            var appealType = await _context.AppealTypes.FindAsync(request.AppealTypeId);

            if (appealType is null)
                throw new NotFoundException(nameof(appealType), request.AppealTypeId);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
