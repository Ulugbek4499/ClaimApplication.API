using AutoMapper;
using ClaimApplication.Application.Commons.Exceptions;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Domain.Entities;
using MediatR;

namespace ClaimApplication.Application.UseCases.TypeOfResponsiblePeople.Commands.DeleteTypeOfResponsiblePerson
{
    public record DeleteTypeOfResponsiblePersonCommand(int Id) : IRequest;

    public class DeleteTypeOfResponsiblePersonCommandHandler : IRequestHandler<DeleteTypeOfResponsiblePersonCommand>
    {
        private IApplicationDbContext _dbContext;
        private IMapper _mapper;

        public DeleteTypeOfResponsiblePersonCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Handle(DeleteTypeOfResponsiblePersonCommand request, CancellationToken cancellationToken)
        {
            TypeOfResponsiblePerson TypeOfResponsiblePerson = FilterIfTypeOfResponsiblePersonExsists(request.Id);

            _dbContext.TypeOfResponsiblePeople.Remove(TypeOfResponsiblePerson);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private TypeOfResponsiblePerson FilterIfTypeOfResponsiblePersonExsists(int id)
            => _dbContext.TypeOfResponsiblePeople
            .FirstOrDefault(c => c.Id == id)
                ?? throw new NotFoundException(
                    " There is no TypeOfResponsiblePerson with id. ");

    }

}
