using AutoMapper;
using ClaimApplication.Application.Commons.Exceptions;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Application.UseCases.ResponsiblePeople.Response;
using MediatR;

namespace ClaimResponsiblePerson.ResponsiblePerson.UseCases.ResponsiblePeople.Queries.GetResponsiblePersonById
{
    public record GetResponsiblePersonByIdQuery(int Id) : IRequest<ResponsiblePersonResponse>;

    public class GetResponsiblePersonByIdQueryHandler : IRequestHandler<GetResponsiblePersonByIdQuery, ResponsiblePersonResponse>
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public GetResponsiblePersonByIdQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task<ResponsiblePersonResponse> Handle(GetResponsiblePersonByIdQuery request, CancellationToken cancellationToken)
        {
            var ResponsiblePerson = FilterIfResponsiblePersonExsists(request.Id);

            var result = _mapper.Map<ResponsiblePersonResponse>(ResponsiblePerson);
            return await Task.FromResult(result);
        }

        private ClaimApplication.Domain.Entities.ResponsiblePerson FilterIfResponsiblePersonExsists(int id)
             => _dbContext.ResponsiblePeople
                 .Find(id)
                      ?? throw new NotFoundException(
                             " There is no Responsible Person with this Id. ");
    }
}
