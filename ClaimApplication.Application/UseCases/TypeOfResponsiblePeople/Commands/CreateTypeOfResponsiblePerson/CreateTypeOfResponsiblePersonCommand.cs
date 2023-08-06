using AutoMapper;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Domain.Entities;
using MediatR;

namespace ClaimApplication.Application.UseCases.TypeOfResponsiblePeople.Commands.CreateTypeOfResponsiblePerson
{
    public class CreateTypeOfResponsiblePersonCommand : IRequest<int>
    {
        public string Name { get; set; }
    }
    public class CreateTypeOfResponsiblePersonCommandHandler : IRequestHandler<CreateTypeOfResponsiblePersonCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public CreateTypeOfResponsiblePersonCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<int> Handle(CreateTypeOfResponsiblePersonCommand request, CancellationToken cancellationToken)
        {
            TypeOfResponsiblePerson TypeOfResponsiblePerson = _mapper.Map<TypeOfResponsiblePerson>(request);
            await _context.TypeOfResponsiblePeople.AddAsync(TypeOfResponsiblePerson, cancellationToken);
            await _context.SaveChangesAsync();

            return TypeOfResponsiblePerson.Id;
        }
    }
}
