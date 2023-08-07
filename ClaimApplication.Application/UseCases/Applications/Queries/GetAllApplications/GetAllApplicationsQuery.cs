using AutoMapper;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Application.UseCases.Applications.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<ApplicationResponse>> Handle(GetAllApplicationsQuery request, CancellationToken cancellationToken)
        {
            var Applications = await _context.Applications.ToListAsync();

            return _mapper.Map<IEnumerable<ApplicationResponse>>(Applications);
        }
    }
}
