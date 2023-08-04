using AutoMapper;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Application.UseCases.TypeOfResponsiblePeople.Response;
using ClaimApplication.Domain.Entities;
using MediatR;

namespace ClaimApplication.Application.UseCases.TypeOfResponsiblePeople.Queries.GetAllTypeOfResponsiblePeople
{
    public record GetAllTypeOfResponsiblePersonsQuery : IRequest<IEnumerable<TypeOfResponsiblePersonResponse>>;

    public class GetAllTypeOfResponsiblePersonsQueryHandler : IRequestHandler<GetAllTypeOfResponsiblePersonsQuery, IEnumerable<TypeOfResponsiblePersonResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetAllTypeOfResponsiblePersonsQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public Task<IEnumerable<TypeOfResponsiblePersonResponse>> Handle(GetAllTypeOfResponsiblePersonsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<TypeOfResponsiblePerson> TypeOfResponsiblePersons = _context.TypeOfResponsiblePeople;

            return Task.FromResult(_mapper.Map<IEnumerable<TypeOfResponsiblePersonResponse>>(TypeOfResponsiblePersons));
        }
    }
}
