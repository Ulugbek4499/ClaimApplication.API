using AutoMapper;
using ClaimApplication.Application.Commons.Exceptions;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Application.UseCases.Applications.Response;
using MediatR;

namespace ClaimApplication.Application.UseCases.Applications.Queries.GetApplicationById
{
    public record GetApplicationByIdQuery(Guid Id) : IRequest<ApplicationResponse>;

    public class GetApplicationByIdQueryHandler : IRequestHandler<GetApplicationByIdQuery, ApplicationResponse>
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public GetApplicationByIdQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task<ApplicationResponse> Handle(GetApplicationByIdQuery request, CancellationToken cancellationToken)
        {
            var Application = FilterIfApplicationExsists(request.Id);

            var result = _mapper.Map<ApplicationResponse>(Application);
            return await Task.FromResult(result);
        }

        private Domain.Entities.Application FilterIfApplicationExsists(Guid id)
            => _dbContext.Applications
                .Find(id)
                     ?? throw new NotFoundException(
                            " There is no Application with this Id. ");
    }
}
