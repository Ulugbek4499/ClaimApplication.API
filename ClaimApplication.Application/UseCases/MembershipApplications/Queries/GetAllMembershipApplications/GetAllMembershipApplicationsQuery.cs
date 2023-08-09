using AutoMapper;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Application.UseCases.AppealTypes.Response;
using ClaimApplication.Domain.Entities;
using MediatR;

namespace ClaimApplication.Application.UseCases.Memberships.Queries.GetAllMemberships
{
    public record GetMembershipApplicationsQuery : IRequest<IEnumerable<AppealTypeResponse>>;

    public class GetMembershipApplicationsQueryHandler : IRequestHandler<GetMembershipApplicationsQuery, IEnumerable<AppealTypeResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetMembershipApplicationsQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public Task<IEnumerable<AppealTypeResponse>> Handle(GetMembershipApplicationsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<AppealType> AppealTypes = _context.AppealTypes;

            return Task.FromResult(_mapper.Map<IEnumerable<AppealTypeResponse>>(AppealTypes));
        }
    }
}
