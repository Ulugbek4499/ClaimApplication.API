using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ClaimApplication.Application.Commons.Interfaces;
using MediatR;

namespace ClaimApplication.Application.UseCases.AppealPredmets.Commands.CreateAppealPredmet
{
    public class CreateAppealPredmetCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public double Discount { get; set; }
        public string Description { get; set; }
        public string? Picture { get; set; }
        public Guid AppealPredmetTypeId { get; set; }
    }
    public class CreateAppealPredmetCommandHandler : IRequestHandler<CreateAppealPredmetCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public CreateAppealPredmetCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;

            _context = context;
        }

        public async Task<Guid> Handle(CreateAppealPredmetCommand request, CancellationToken cancellationToken)
        {
            AppealPredmet AppealPredmet = _mapper.Map<AppealPredmet>(request);
            await _context.AppealPredmets.AddAsync(AppealPredmet, cancellationToken);
            await _context.SaveChangesAsync();

            return AppealPredmet.Id;
        }
    }
}
