using AutoMapper;
using ClaimApplication.Application.Commons.Exceptions;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Application.UseCases.AppealTypes.Response;
using ClaimApplication.Domain.Entities;
using MediatR;

namespace ClaimApplication.Application.UseCases.AppealTypes.Queries.GetAppealTypeById
{
    public record GetAppealTypeByIdQuery(Guid Id) : IRequest<AppealTypeResponse>;

    public class GetAppealTypeByIdQueryHandler : IRequestHandler<GetAppealTypeByIdQuery, AppealTypeResponse>
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public GetAppealTypeByIdQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task<AppealTypeResponse> Handle(GetAppealTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var AppealType = FilterIfAppealTypeExsists(request.Id);

            var result = _mapper.Map<AppealTypeResponse>(AppealType);
            return await Task.FromResult(result);
        }

        private AppealType FilterIfAppealTypeExsists(Guid id)
            => _dbContext.AppealTypes
                .Find(id)
                     ?? throw new NotFoundException(
                            " There is no AppealType with this Id. ");
    }
}
