using AutoMapper;
using ClaimApplication.Application.Commons.Exceptions;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Application.Commons.Models;
using ClaimApplication.Application.UseCases.TypeOfResponsiblePeople.Response;
using ClaimApplication.Domain.Entities;
using MediatR;

namespace ClaimApplication.Application.UseCases.TypeOfResponsiblePeople.Queries.GetTypeOfResponsiblePeoplePagination
{
    public record GetTypeOfResponsiblePersonsPaginationQuery : IRequest<PaginatedList<TypeOfResponsiblePersonResponse>>
    {
        public string? SearchTerm { get; init; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetTypeOfResponsiblePersonsPaginationQueryHandler : IRequestHandler<GetTypeOfResponsiblePersonsPaginationQuery,
        PaginatedList<TypeOfResponsiblePersonResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetTypeOfResponsiblePersonsPaginationQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<PaginatedList<TypeOfResponsiblePersonResponse>> Handle(
            GetTypeOfResponsiblePersonsPaginationQuery request, CancellationToken cancellationToken)
        {
            var search = request.SearchTerm?.Trim();
            var TypeOfResponsiblePersons = _context.TypeOfResponsiblePeople.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                TypeOfResponsiblePersons = TypeOfResponsiblePersons.Where(s => s.Name.ToLower().Contains(search.ToLower()));
            }
            if (TypeOfResponsiblePersons is null || TypeOfResponsiblePersons.Count() <= 0)
            {
                throw new NotFoundException(nameof(TypeOfResponsiblePerson), search);
            }

            var paginatedTypeOfResponsiblePersons = await PaginatedList<TypeOfResponsiblePerson>.CreateAsync(
                TypeOfResponsiblePersons, request.PageNumber, request.PageSize);

            var response = _mapper.Map<List<TypeOfResponsiblePersonResponse>>(paginatedTypeOfResponsiblePersons.Items);

            var result = new PaginatedList<TypeOfResponsiblePersonResponse>
                (response, paginatedTypeOfResponsiblePersons.TotalCount, request.PageNumber, request.PageSize);

            return result;
        }
    }
}
