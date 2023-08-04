using AutoMapper;
using ClaimApplication.Application.Commons.Exceptions;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Application.UseCases.AppealPredmets.Response;
using ClaimApplication.Domain.Entities;
using MediatR;

namespace ClaimApplication.Application.UseCases.AppealPredmets.Queries.GetAppealPredmetById
{
    public record GetAppealPredmetByIdQuery(Guid Id) : IRequest<AppealPredmetResponse>;

    public class GetAppealPredmetByIdQueryHandler : IRequestHandler<GetAppealPredmetByIdQuery, AppealPredmetResponse>
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public GetAppealPredmetByIdQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task<AppealPredmetResponse> Handle(GetAppealPredmetByIdQuery request, CancellationToken cancellationToken)
        {
            var AppealPredmet = FilterIfAppealPredmetExsists(request.Id);

            var result = _mapper.Map<AppealPredmetResponse>(AppealPredmet);
            return await Task.FromResult(result);
        }

        private AppealPredmet FilterIfAppealPredmetExsists(Guid id)
            => _dbContext.AppealPredmets
                .Find(id)
                     ?? throw new NotFoundException(
                            " There is no AppealPredmet with this Id. ");
    }
}
