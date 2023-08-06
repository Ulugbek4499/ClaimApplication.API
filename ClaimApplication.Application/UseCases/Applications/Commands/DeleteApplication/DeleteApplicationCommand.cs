using AutoMapper;
using ClaimApplication.Application.Commons.Exceptions;
using ClaimApplication.Application.Commons.Interfaces;
using MediatR;

namespace ClaimApplication.Application.UseCases.Applications.Commands.DeleteApplication
{
    public record DeleteApplicationCommand(int Id) : IRequest;

    public class DeleteApplicationCommandHandler : IRequestHandler<DeleteApplicationCommand>
    {
        private IApplicationDbContext _dbContext;
        private IMapper _mapper;

        public DeleteApplicationCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Handle(DeleteApplicationCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Application Application = FilterIfApplicationExsists(request.Id);

            _dbContext.Applications.Remove(Application);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private Domain.Entities.Application FilterIfApplicationExsists(int id)
            => _dbContext.Applications
            .FirstOrDefault(c => c.Id == id)
                ?? throw new NotFoundException(
                    " There is no Application with id. ");

    }

}
