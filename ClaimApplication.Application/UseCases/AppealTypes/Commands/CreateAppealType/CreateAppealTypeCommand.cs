using AutoMapper;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Domain.Entities;
using MediatR;

namespace ClaimApplication.Application.UseCases.AppealTypes.Commands.CreateAppealType
{
    public class CreateAppealTypeCommand : IRequest<Guid>
    {
        public string Name { get; set; }
    }
    public class CreateAppealTypeCommandHandler : IRequestHandler<CreateAppealTypeCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public CreateAppealTypeCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Guid> Handle(CreateAppealTypeCommand request, CancellationToken cancellationToken)
        {
            AppealType AppealType = _mapper.Map<AppealType>(request);
            await _context.AppealTypes.AddAsync(AppealType, cancellationToken);
            await _context.SaveChangesAsync();

            return AppealType.Id;
        }
    }
}
