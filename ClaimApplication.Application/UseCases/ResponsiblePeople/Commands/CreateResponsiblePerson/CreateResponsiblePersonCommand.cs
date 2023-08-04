using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Domain.Entities;
using MediatR;

namespace ClaimApplication.Application.UseCases.ResponsiblePeople.Commands.CreateResponsiblePerson
{
    public class CreateResponsiblePersonCommand : IRequest<Guid>
    {
        public string OrdinalNumber { get; set; } = null!;
        public string Inn { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

        public Guid ApplicationId { get; set; }
        public Guid TypeOfResponsiblePersonId { get; set; }
    }
    public class CreateResponsiblePersonCommandHandler : IRequestHandler<CreateResponsiblePersonCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public CreateResponsiblePersonCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Guid> Handle(CreateResponsiblePersonCommand request, CancellationToken cancellationToken)
        {
            ResponsiblePerson responsiblePerson = _mapper.Map<ResponsiblePerson>(request);
            await _context.ResponsiblePeople.AddAsync(responsiblePerson, cancellationToken);
            await _context.SaveChangesAsync();

            return responsiblePerson.Id;
        }
    }
}
