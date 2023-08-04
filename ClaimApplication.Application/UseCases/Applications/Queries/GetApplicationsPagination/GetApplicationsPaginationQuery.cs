using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ClaimApplication.Application.Commons.Exceptions;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Application.Commons.Models;
using ClaimApplication.Application.UseCases.Applications.Response;
using ClaimApplication.Domain.Entities;
using MediatR;

namespace ClaimApplication.Application.UseCases.Applications.Queries.GetApplicationsPagination
{
    public record GetApplicationsPaginationQuery : IRequest<PaginatedList<ApplicationResponse>>
    {
        public string? SearchTerm { get; init; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetApplicationsPaginationQueryHandler : IRequestHandler<GetApplicationsPaginationQuery,
        PaginatedList<ApplicationResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetApplicationsPaginationQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<PaginatedList<ApplicationResponse>> Handle(
            GetApplicationsPaginationQuery request, CancellationToken cancellationToken)
        {
            var search = request.SearchTerm?.Trim();
            var Applications = _context.Applications.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                Applications = Applications.Where(s => s.AppealText.ToLower().Contains(search.ToLower()));
            }
            if (Applications is null || Applications.Count() <= 0)
            {
                throw new NotFoundException(nameof(Application), search);
            }

            var paginatedApplications = await PaginatedList<Domain.Entities.Application>.CreateAsync(
                Applications, request.PageNumber, request.PageSize);

            var response = _mapper.Map<List<ApplicationResponse>>(paginatedApplications.Items);

            var result = new PaginatedList<ApplicationResponse>
                (response, paginatedApplications.TotalCount, request.PageNumber, request.PageSize);

            return result;
        }
    }
}
