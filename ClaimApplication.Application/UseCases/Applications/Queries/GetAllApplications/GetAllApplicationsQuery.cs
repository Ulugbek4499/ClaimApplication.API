using AutoMapper;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Application.UseCases.Applications.Response;
using MediatR;

namespace ClaimApplication.Application.UseCases.Applications.Queries.GetAllApplications
{
    public record GetAllApplicationsQuery : IRequest<IEnumerable<ApplicationResponse>>;

    public class GetAllApplicationsQueryHandler : IRequestHandler<GetAllApplicationsQuery, IEnumerable<ApplicationResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetAllApplicationsQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public Task<IEnumerable<ApplicationResponse>> Handle(GetAllApplicationsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Domain.Entities.Application> Applications = _context.Applications;

            return Task.FromResult(_mapper.Map<IEnumerable<ApplicationResponse>>(Applications));
        }
    }
}
