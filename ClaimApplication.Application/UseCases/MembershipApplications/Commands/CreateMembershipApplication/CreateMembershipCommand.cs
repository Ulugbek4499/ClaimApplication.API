using AutoMapper;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Domain.Memberships;
using MediatR;

namespace ClaimMembershipApplication.MembershipApplication.UseCases.MembershipApplications.Commands.CreateMembershipApplication
{
    public class CreateMembershipApplicationCommand : IRequest<int>
    {
        public string? NameOfBusiness { get; set; }
        public string? FullNameOfManager { get; set; }
        public Gender? Gender { get; set; }
        public string? Address { get; set; }
        public string? PostIndex { get; set; }
        public string? PhoneNumber { get; set; }
        public string? MobileNumberFirst { get; set; }
        public string? MobileNumberSecond { get; set; }
        public string? Fax { get; set; }
        public string? MobileNumberExtra { get; set; }
        public string? EmailFirst { get; set; }
        public string? EmailSecond { get; set; }
        public string? WebSite { get; set; }
        public string? SkypeProfile { get; set; }
        public string? FaceBookProfile { get; set; }
        public string? TelegramProfile { get; set; }
        public string? ExtraProfile { get; set; }
        public DateTime? BussinessRegesteredDate { get; set; }
        public string? RegistrationNumber { get; set; }
        public bool? HasCopyOfRegistrationCerteficate { get; set; }
        public string? Inn { get; set; }
        public string? OKED { get; set; }

        public int? NumberOfEmployees { get; set; }
        public string? NameOfBank { get; set; }
        public string? CodeOfBank { get; set; }
        public string? BankAccount { get; set; }
        public decimal? AnnualTurnoverOfEnterprise { get; set; }
        public decimal? AnnualPaidTax { get; set; }
        public decimal? AnnualExportAmount { get; set; }
        public decimal? AnnualImportAmount { get; set; }
        public decimal? AnnualProductionAmount { get; set; }
        public string? BrandName { get; set; }
        public string? BithDateOfManager { get; set; }
        public string? SeriesOfPassport { get; set; }
        public string? NumberOfPassport { get; set; }
        public string? PassportGivenFrom { get; set; }
        public string? Nationality { get; set; }
        public string? EducationDegree { get; set; }
        public string? ExtraInformation { get; set; }

        public int? ForeignLanguageId { get; set; }
        public int? MainActivityId { get; set; }
        public int? BussinessCategoryId { get; set; }
    }
    public class CreateMembershipApplicationCommandHandler : IRequestHandler<CreateMembershipApplicationCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public CreateMembershipApplicationCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<int> Handle(CreateMembershipApplicationCommand request, CancellationToken cancellationToken)
        {
            ClaimApplication.Domain.Memberships.MembershipApplication MembershipApplication = _mapper.Map<ClaimApplication.Domain.Memberships.MembershipApplication>(request);
            await _context.MembershipApplications.AddAsync(MembershipApplication, cancellationToken);
            await _context.SaveChangesAsync();

            return MembershipApplication.Id;
        }
    }
}
