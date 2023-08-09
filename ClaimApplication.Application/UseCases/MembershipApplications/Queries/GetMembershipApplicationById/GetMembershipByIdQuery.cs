using AutoMapper;
using ClaimApplication.Application.Commons.Exceptions;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Application.UseCases.Applications.Response;
using ClaimApplication.Domain.Memberships;
using MediatR;

namespace ClaimApplication.Application.UseCases.Memberships.Queries.GetMembershipById
{
    public record GetMembershipApplicationByIdQuery(int Id) : IRequest<MembershipApplicationResponse>;

    public class GetMembershipApplicationByIdQueryHandler : IRequestHandler<GetMembershipApplicationByIdQuery, MembershipApplicationResponse>
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public GetMembershipApplicationByIdQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task<MembershipApplicationResponse> Handle(GetMembershipApplicationByIdQuery request, CancellationToken cancellationToken)
        {
            var membershipApplication = FilterIfMembershipApplicationExsists(request.Id);

            var result = _mapper.Map<MembershipApplicationResponse>(membershipApplication);
            return await Task.FromResult(result);
        }

        private MembershipApplication FilterIfMembershipApplicationExsists(int id)
            => _dbContext.MembershipApplications
                .Find(id)
                     ?? throw new NotFoundException(
                            " There is no MembershipApplication with this Id. ");
    }
}
