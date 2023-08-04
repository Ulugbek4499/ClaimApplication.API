using AutoMapper;
using ClaimApplication.Application.Commons.Exceptions;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Domain.Entities;
using MediatR;

namespace ClaimApplication.Application.UseCases.TypeOfResponsiblePeople.Commands.UpdateTypeOfResponsiblePerson
{
    public class UpdateTypeOfResponsiblePersonCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

    }
    public class UpdateTypeOfResponsiblePersonCommandHandler : IRequestHandler<UpdateTypeOfResponsiblePersonCommand>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public UpdateTypeOfResponsiblePersonCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task Handle(UpdateTypeOfResponsiblePersonCommand request, CancellationToken cancellationToken)
        {
            TypeOfResponsiblePerson? TypeOfResponsiblePerson = await _context.TypeOfResponsiblePeople.FindAsync(request.Id);

            _mapper.Map(request, TypeOfResponsiblePerson);

            if (TypeOfResponsiblePerson is null)
                throw new NotFoundException(nameof(TypeOfResponsiblePerson), request.Id);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
