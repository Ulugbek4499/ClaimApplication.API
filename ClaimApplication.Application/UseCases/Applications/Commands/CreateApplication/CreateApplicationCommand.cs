using AutoMapper;
using ClaimApplication.Application.Commons.Interfaces;
using MediatR;

namespace ClaimApplication.Application.UseCases.Applications.Commands.CreateApplication
{
    public class CreateApplicationCommand : IRequest<Guid>
    {
        public Guid ProductId { get; set; }
        public Guid SupplierId { get; set; }
        public double IncomingCount { get; set; }
        public double IncomingPrice { get; set; }
        public double Markup { get; set; }
        public DateTime IncomingDate { get; set; }
    }
    public class CreateApplicationCommandHandler : IRequestHandler<CreateApplicationCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public CreateApplicationCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Guid> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Application Application = _mapper.Map<Domain.Entities.Application>(request);
            await _context.Applications.AddAsync(Application, cancellationToken);
            await _context.SaveChangesAsync();

            return Application.Id;
        }
    }
}
