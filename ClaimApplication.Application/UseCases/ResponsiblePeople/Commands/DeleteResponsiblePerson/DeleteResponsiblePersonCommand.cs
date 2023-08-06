using AutoMapper;
using ClaimApplication.Application.Commons.Exceptions;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Domain.Entities;
using MediatR;

namespace ClaimApplication.Application.UseCases.ResponsiblePeople.Commands.DeleteResponsiblePerson
{
    public record DeleteResponsiblePersonCommand(int Id) : IRequest;

    public class DeleteResponsiblePersonCommandHandler : IRequestHandler<DeleteResponsiblePersonCommand>
    {
        private IApplicationDbContext _dbContext;
        private IMapper _mapper;

        public DeleteResponsiblePersonCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Handle(DeleteResponsiblePersonCommand request, CancellationToken cancellationToken)
        {
            ResponsiblePerson ResponsiblePerson = FilterIfResponsiblePersonExsists(request.Id);

            _dbContext.ResponsiblePeople.Remove(ResponsiblePerson);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private ResponsiblePerson FilterIfResponsiblePersonExsists(int id)
            => _dbContext.ResponsiblePeople
            .FirstOrDefault(c => c.Id == id)
                ?? throw new NotFoundException(
                    " There is no ResponsiblePerson with id. ");

    }

}
