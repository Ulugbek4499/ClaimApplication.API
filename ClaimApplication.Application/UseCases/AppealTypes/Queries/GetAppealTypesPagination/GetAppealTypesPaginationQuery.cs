using AutoMapper;
using ClaimApplication.Application.Commons.Exceptions;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Application.Commons.Models;
using ClaimApplication.Application.UseCases.AppealTypes.Response;
using ClaimApplication.Domain.Entities;
using MediatR;

namespace ClaimApplication.Application.UseCases.AppealTypes.Queries.GetAppealTypesPagination
{
    public record GetAppealTypesPaginationQuery : IRequest<PaginatedList<AppealTypeResponse>>
    {
        public string? SearchTerm { get; init; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetAppealTypesPaginationQueryHandler : IRequestHandler<GetAppealTypesPaginationQuery,
        PaginatedList<AppealTypeResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetAppealTypesPaginationQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<PaginatedList<AppealTypeResponse>> Handle(
            GetAppealTypesPaginationQuery request, CancellationToken cancellationToken)
        {
            var search = request.SearchTerm?.Trim();
            var AppealTypes = _context.AppealTypes.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                AppealTypes = AppealTypes.Where(s => s.Name.ToLower().Contains(search.ToLower()));
            }
            if (AppealTypes is null || AppealTypes.Count() <= 0)
            {
                throw new NotFoundException(nameof(AppealType), search);
            }

            var paginatedAppealTypes = await PaginatedList<AppealType>.CreateAsync(
                AppealTypes, request.PageNumber, request.PageSize);

            var response = _mapper.Map<List<AppealTypeResponse>>(paginatedAppealTypes.Items);

            var result = new PaginatedList<AppealTypeResponse>
                (response, paginatedAppealTypes.TotalCount, request.PageNumber, request.PageSize);

            return result;
        }
    }
}
