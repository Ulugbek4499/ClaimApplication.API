using AutoMapper;
using ClaimApplication.Application.Commons.Exceptions;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Application.Commons.Models;
using ClaimApplication.Application.UseCases.ResponsiblePeople.Response;
using ClaimApplication.Domain.Entities;
using MediatR;

namespace ClaimApplication.Application.UseCases.ResponsiblePeople.Queries.GetResponsiblePeoplePagination
{
    public record GetResponsiblePeoplePaginationQuery : IRequest<PaginatedList<ResponsiblePersonResponse>>
    {
        public string? SearchTerm { get; init; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetResponsiblePeoplePaginationQueryHandler : IRequestHandler<GetResponsiblePeoplePaginationQuery,
        PaginatedList<ResponsiblePersonResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetResponsiblePeoplePaginationQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<PaginatedList<ResponsiblePersonResponse>> Handle(
            GetResponsiblePeoplePaginationQuery request, CancellationToken cancellationToken)
        {
            var search = request.SearchTerm?.Trim();
            var ResponsiblePeople = _context.ResponsiblePeople.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                ResponsiblePeople = ResponsiblePeople.Where(s =>
                                                                 s.Id.ToString().Contains(search.ToLower()) ||
                                                                 s.OrdinalNumber.Contains(search.ToLower()) ||
                                                                 s.Inn.ToLower().Contains(search.ToLower()) ||
                                                                 s.FullName.ToLower().Contains(search.ToLower()) ||
                                                                 s.Address.ToLower().Contains(search.ToLower()) ||
                                                                 s.PhoneNumber.ToLower().Contains(search.ToLower()) ||
                                                                 s.ApplicationId.ToString().ToLower().Contains(search.ToLower()) ||
                                                                 s.TypeOfResponsiblePersonId.ToString().ToLower().Contains(search.ToLower())
                                                             );
            }
            if (ResponsiblePeople is null || ResponsiblePeople.Count() <= 0)
            {
                throw new NotFoundException(nameof(ResponsiblePerson), search);
            }

            var paginatedResponsiblePeople = await PaginatedList<ResponsiblePerson>.CreateAsync(
                ResponsiblePeople, request.PageNumber, request.PageSize);

            var response = _mapper.Map<List<ResponsiblePersonResponse>>(paginatedResponsiblePeople.Items);

            var result = new PaginatedList<ResponsiblePersonResponse>
                (response, paginatedResponsiblePeople.TotalCount, request.PageNumber, request.PageSize);

            return result;
        }
    }
}
