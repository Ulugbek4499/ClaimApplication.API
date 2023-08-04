using AutoMapper;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Domain.Entities;
using MediatR;

namespace ClaimApplication.Application.UseCases.AppealPredmets.Commands.CreateAppealPredmet
{
    public class CreateAppealPredmetCommand : IRequest<Guid>
    {
        public string Name { get; set; }
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
            AppealPredmet appealPredmet = _mapper.Map<AppealPredmet>(request);
            await _context.AppealPredmets.AddAsync(appealPredmet, cancellationToken);
            await _context.SaveChangesAsync();

            return appealPredmet.Id;
        }
    }
}
