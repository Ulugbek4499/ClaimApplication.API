using AutoMapper;
using ClaimApplication.Application.Commons.Exceptions;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Domain.Entities;
using MediatR;

namespace ClaimApplication.Application.UseCases.ResponsiblePeople.Commands.UpdateResponsiblePerson
{
    public class UpdateResponsiblePersonCommand : IRequest
    {
        public Guid Id { get; set; }
        public string OrdinalNumber { get; set; } = null!;
        public string Inn { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

        public Guid ApplicationId { get; set; }
        public Guid TypeOfResponsiblePersonId { get; set; }
    }
    public class UpdateResponsiblePersonCommandHandler : IRequestHandler<UpdateResponsiblePersonCommand>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public UpdateResponsiblePersonCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task Handle(UpdateResponsiblePersonCommand request, CancellationToken cancellationToken)
        {
            ResponsiblePerson? responsiblePerson = await _context.ResponsiblePeople.FindAsync(request.Id);
            _mapper.Map(request, responsiblePerson);

            if (responsiblePerson is null)
                throw new NotFoundException(nameof(ResponsiblePerson), request.Id);

            var application = await _context.Applications.FindAsync(request.ApplicationId);

            if (application is null)
                throw new NotFoundException(nameof(application), request.ApplicationId);

            var typeOfResponsiblePerson = await _context.TypeOfResponsiblePeople.FindAsync(request.TypeOfResponsiblePersonId);

            if (typeOfResponsiblePerson is null)
                throw new NotFoundException(nameof(TypeOfResponsiblePerson), request.TypeOfResponsiblePersonId);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
