using AutoMapper;
using ClaimApplication.Application.Commons.Exceptions;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Domain.Entities;
using MediatR;

namespace ClaimApplication.Application.UseCases.AppealPredmets.Commands.UpdateAppealPredmet
{
    public class UpdateAppealPredmetCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
    public class UpdateAppealPredmetCommandHandler : IRequestHandler<UpdateAppealPredmetCommand>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public UpdateAppealPredmetCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task Handle(UpdateAppealPredmetCommand request, CancellationToken cancellationToken)
        {

            AppealPredmet? appealPredmet = await _context.AppealPredmets.FindAsync(request.Id);
            _mapper.Map(request, appealPredmet);

            if (appealPredmet is null)
                throw new NotFoundException(nameof(AppealPredmet), request.Id);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
