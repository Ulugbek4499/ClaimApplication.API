using AutoMapper;
using ClaimApplication.Application.Commons.Exceptions;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Domain.Entities;
using MediatR;

namespace ClaimApplication.Application.UseCases.AppealTypes.Commands.UpdateAppealType
{
    public class UpdateAppealTypeCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

    }
    public class UpdateAppealTypeCommandHandler : IRequestHandler<UpdateAppealTypeCommand>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public UpdateAppealTypeCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task Handle(UpdateAppealTypeCommand request, CancellationToken cancellationToken)
        {
            AppealType? AppealType = await _context.AppealTypes.FindAsync(request.Id);

            _mapper.Map(request, AppealType);

            if (AppealType is null)
                throw new NotFoundException(nameof(AppealType), request.Id);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
