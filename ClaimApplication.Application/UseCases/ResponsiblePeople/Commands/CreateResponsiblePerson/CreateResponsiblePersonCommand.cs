using AutoMapper;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Domain.Entities;
using MediatR;

namespace ClaimApplication.Application.UseCases.ResponsiblePeople.Commands.CreateResponsiblePerson
{
    public class CreateResponsiblePersonCommand : IRequest<int>
    {
        public string OrdinalNumber { get; set; }
        public string Inn { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; } 
        public string PhoneNumber { get; set; }

        public int ApplicationId { get; set; }
        public int TypeOfResponsiblePersonId { get; set; }
    }
    public class CreateResponsiblePersonCommandHandler : IRequestHandler<CreateResponsiblePersonCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public CreateResponsiblePersonCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<int> Handle(CreateResponsiblePersonCommand request, CancellationToken cancellationToken)
        {
            ResponsiblePerson responsiblePerson = _mapper.Map<ResponsiblePerson>(request);
            await _context.ResponsiblePeople.AddAsync(responsiblePerson, cancellationToken);
            await _context.SaveChangesAsync();

            return responsiblePerson.Id;
        }
    }
}
