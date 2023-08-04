using AutoMapper;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Application.UseCases.AppealTypes.Response;
using ClaimApplication.Domain.Entities;
using MediatR;

namespace ClaimApplication.Application.UseCases.AppealTypes.Queries.GetAllAppealTypes
{
    public record GetAllAppealTypesQuery : IRequest<IEnumerable<AppealTypeResponse>>;

    public class GetAllAppealTypesQueryHandler : IRequestHandler<GetAllAppealTypesQuery, IEnumerable<AppealTypeResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetAllAppealTypesQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public Task<IEnumerable<AppealTypeResponse>> Handle(GetAllAppealTypesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<AppealType> AppealTypes = _context.AppealTypes;

            return Task.FromResult(_mapper.Map<IEnumerable<AppealTypeResponse>>(AppealTypes));
        }
    }
}
