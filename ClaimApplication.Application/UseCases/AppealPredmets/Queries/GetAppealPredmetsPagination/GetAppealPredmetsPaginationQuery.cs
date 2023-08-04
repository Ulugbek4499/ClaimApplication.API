using AutoMapper;
using ClaimApplication.Application.Commons.Exceptions;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Application.Commons.Models;
using ClaimApplication.Application.UseCases.AppealPredmets.Response;
using ClaimApplication.Domain.Entities;
using MediatR;

namespace ClaimApplication.Application.UseCases.AppealPredmets.Queries.GetAppealPredmetsPagination
{
    public record GetAppealPredmetsPaginationQuery : IRequest<PaginatedList<AppealPredmetResponse>>
    {
        public string? SearchTerm { get; init; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetAppealPredmetsPaginationQueryHandler : IRequestHandler<GetAppealPredmetsPaginationQuery,
        PaginatedList<AppealPredmetResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetAppealPredmetsPaginationQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<PaginatedList<AppealPredmetResponse>> Handle(
            GetAppealPredmetsPaginationQuery request, CancellationToken cancellationToken)
        {
            var search = request.SearchTerm?.Trim();
            var AppealPredmets = _context.AppealPredmets.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                AppealPredmets = AppealPredmets.Where(s => s.Name.ToLower().Contains(search.ToLower()));
            }
            if (AppealPredmets is null || AppealPredmets.Count() <= 0)
            {
                throw new NotFoundException(nameof(AppealPredmet), search);
            }

            var paginatedAppealPredmets = await PaginatedList<AppealPredmet>.CreateAsync(
                AppealPredmets, request.PageNumber, request.PageSize);

            var response = _mapper.Map<List<AppealPredmetResponse>>(paginatedAppealPredmets.Items);

            var result = new PaginatedList<AppealPredmetResponse>
                (response, paginatedAppealPredmets.TotalCount, request.PageNumber, request.PageSize);

            return result;
        }
    }
}
