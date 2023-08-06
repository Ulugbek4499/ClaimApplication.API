using AutoMapper;
using ClaimApplication.Application.Commons.Exceptions;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Application.UseCases.TypeOfResponsiblePeople.Response;
using ClaimApplication.Domain.Entities;
using MediatR;

namespace ClaimApplication.Application.UseCases.TypeOfResponsiblePeople.Queries.GetTypeOfResponsiblePersonById
{
    public record GetTypeOfResponsiblePersonByIdQuery(int Id) : IRequest<TypeOfResponsiblePersonResponse>;

    public class GetTypeOfResponsiblePersonByIdQueryHandler : IRequestHandler<GetTypeOfResponsiblePersonByIdQuery, TypeOfResponsiblePersonResponse>
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public GetTypeOfResponsiblePersonByIdQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task<TypeOfResponsiblePersonResponse> Handle(GetTypeOfResponsiblePersonByIdQuery request, CancellationToken cancellationToken)
        {
            var TypeOfResponsiblePerson = FilterIfTypeOfResponsiblePersonExsists(request.Id);

            var result = _mapper.Map<TypeOfResponsiblePersonResponse>(TypeOfResponsiblePerson);
            return await Task.FromResult(result);
        }

        private TypeOfResponsiblePerson FilterIfTypeOfResponsiblePersonExsists(int id)
            => _dbContext.TypeOfResponsiblePeople
                .Find(id)
                     ?? throw new NotFoundException(
                            " There is no TypeOfResponsiblePerson with this Id. ");
    }
}
