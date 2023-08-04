using AutoMapper;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Application.UseCases.ResponsiblePeople.Response;
using ClaimApplication.Domain.Entities;
using MediatR;

namespace ClaimResponsiblePeople.ResponsiblePeople.UseCases.ResponsiblePeople.Queries.GetAllResponsiblePeople
{
    public record GetAllResponsiblePeopleQuery : IRequest<IEnumerable<ResponsiblePersonResponse>>;

    public class GetAllResponsiblePeopleQueryHandler : IRequestHandler<GetAllResponsiblePeopleQuery, IEnumerable<ResponsiblePersonResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetAllResponsiblePeopleQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public Task<IEnumerable<ResponsiblePersonResponse>> Handle(GetAllResponsiblePeopleQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<ResponsiblePerson> ResponsiblePeople = _context.ResponsiblePeople;

            return Task.FromResult(_mapper.Map<IEnumerable<ResponsiblePersonResponse>>(ResponsiblePeople));
        }
    }
}
