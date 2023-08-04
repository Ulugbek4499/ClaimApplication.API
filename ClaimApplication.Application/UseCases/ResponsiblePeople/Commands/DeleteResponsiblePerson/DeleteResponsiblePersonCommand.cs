using ClaimApplication.Application.Commons.Exceptions;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Domain.Entities;
using MediatR;

namespace ClaimApplication.Application.UseCases.ResponsiblePeople.Commands.DeleteResponsiblePerson
{
    public class DeleteResponsiblePersonCommand : IRequest
    {
        public Guid Id { get; set; }
    }
    public class DeleteResponsiblePersonCommandHandler : IRequestHandler<DeleteResponsiblePersonCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteResponsiblePersonCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteResponsiblePersonCommand request, CancellationToken cancellationToken)
        {
            ResponsiblePerson? ResponsiblePerson = await _context.ResponsiblePeople.FindAsync(request.Id);

            if (ResponsiblePerson is null)
                throw new NotFoundException(nameof(ResponsiblePerson), request.Id);
        }
    }
}
