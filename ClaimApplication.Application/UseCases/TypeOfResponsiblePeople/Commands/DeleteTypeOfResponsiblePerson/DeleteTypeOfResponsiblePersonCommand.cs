using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimApplication.Application.Commons.Exceptions;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Domain.Entities;
using MediatR;

namespace ClaimApplication.Application.UseCases.TypeOfResponsiblePeople.Commands.DeleteTypeOfResponsiblePerson
{
    public class DeleteTypeOfResponsiblePersonCommand : IRequest
    {
        public Guid Id { get; set; }
    }
    public class DeleteTypeOfResponsiblePersonCommandHandler : IRequestHandler<DeleteTypeOfResponsiblePersonCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTypeOfResponsiblePersonCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteTypeOfResponsiblePersonCommand request, CancellationToken cancellationToken)
        {
            TypeOfResponsiblePerson? TypeOfResponsiblePerson = await _context.TypeOfResponsiblePeople.FindAsync(request.Id);

            if (TypeOfResponsiblePerson is null)
                throw new NotFoundException(nameof(TypeOfResponsiblePerson), request.Id);
        }
    }
}
