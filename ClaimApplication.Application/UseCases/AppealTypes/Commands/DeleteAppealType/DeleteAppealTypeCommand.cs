using ClaimApplication.Application.Commons.Exceptions;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Domain.Entities;
using MediatR;

namespace ClaimApplication.Application.UseCases.AppealTypes.Commands.DeleteAppealType
{
    public class DeleteAppealTypeCommand : IRequest
    {
        public Guid Id { get; set; }
    }
    public class DeleteAppealTypeCommandHandler : IRequestHandler<DeleteAppealTypeCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteAppealTypeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteAppealTypeCommand request, CancellationToken cancellationToken)
        {
            AppealType? AppealType = await _context.AppealTypes.FindAsync(request.Id);

            if (AppealType is null)
                throw new NotFoundException(nameof(AppealType), request.Id);
        }
    }
}
