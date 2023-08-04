using ClaimApplication.Application.Commons.Exceptions;
using ClaimApplication.Application.Commons.Interfaces;
using MediatR;

namespace ClaimApplication.Application.UseCases.Applications.Commands.DeleteApplication
{
    public class DeleteApplicationCommand : IRequest
    {
        public Guid Id { get; set; }
    }
    public class DeleteApplicationCommandHandler : IRequestHandler<DeleteApplicationCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteApplicationCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteApplicationCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Application? Application = await _context.Applications.FindAsync(request.Id);

            if (Application is null)
                throw new NotFoundException(nameof(Application), request.Id);
        }
    }
}
