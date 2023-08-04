using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Application.UseCases.AppealPredmets.Response;
using MediatR;

namespace ClaimApplication.Application.UseCases.AppealPredmets.Queries.GetAllAppealPredmets
{
    public record GetAllAppealPredmetsQuery : IRequest<IEnumerable<AppealPredmetResponse>>;

    public class GetAllAppealPredmetsQueryHandler : IRequestHandler<GetAllAppealPredmetsQuery, IEnumerable<AppealPredmetResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetAllAppealPredmetsQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public Task<IEnumerable<AppealPredmetResponse>> Handle(GetAllAppealPredmetsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<AppealPredmet> AppealPredmets = _context.AppealPredmets;

            return Task.FromResult(_mapper.Map<IEnumerable<AppealPredmetResponse>>(AppealPredmets));
        }
    }
}
