using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimApplication.Application.Commons.Exceptions;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Domain.Entities;
using MediatR;

namespace ClaimApplication.Application.UseCases.AppealPredmets.Commands.DeleteAppealPredmet
{
    public class DeleteAppealPredmetCommand : IRequest
    {
        public Guid Id { get; set; }
    }
    public class DeleteAppealPredmetCommandHandler : IRequestHandler<DeleteAppealPredmetCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteAppealPredmetCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteAppealPredmetCommand request, CancellationToken cancellationToken)
        {
            AppealPredmet? appealPredmet = await _context.AppealPredmets.FindAsync(request.Id);
            if (appealPredmet is null)
                throw new NotFoundException(nameof(appealPredmet), request.Id);
        }
    }
}
