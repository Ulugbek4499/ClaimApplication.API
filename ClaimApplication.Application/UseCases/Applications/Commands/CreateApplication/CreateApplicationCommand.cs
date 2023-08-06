using AutoMapper;
using ClaimApplication.Application.Commons.Interfaces;
using MediatR;

namespace ClaimApplication.Application.UseCases.Applications.Commands.CreateApplication
{
    public class CreateApplicationCommand : IRequest<Guid>
    {
        public string Inn { get; set; }
        public string OrdinalNumber { get; set; }
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
    public class CreateApplicationCommandHandler : IRequestHandler<CreateApplicationCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public CreateApplicationCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Guid> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Application Application = _mapper.Map<Domain.Entities.Application>(request);
            await _context.Applications.AddAsync(Application, cancellationToken);
            await _context.SaveChangesAsync();

            return Application.Id;
        }
    }
}
