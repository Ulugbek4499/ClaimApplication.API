using AutoMapper;
using ClaimApplication.Application.Commons.Exceptions;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Domain.Entities;
using MediatR;

namespace ClaimApplication.Application.UseCases.AppealPredmets.Commands.DeleteAppealPredmet
{
    public record DeleteAppealPredmetCommand(Guid Id) : IRequest;

    public class DeleteAppealPredmetCommandHandler : IRequestHandler<DeleteAppealPredmetCommand>
    {
        private IApplicationDbContext _dbContext;
        private IMapper _mapper;

        public DeleteAppealPredmetCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Handle(DeleteAppealPredmetCommand request, CancellationToken cancellationToken)
        {
            AppealPredmet AppealPredmet = FilterIfAppealPredmetExsists(request.Id);

            _dbContext.AppealPredmets.Remove(AppealPredmet);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private AppealPredmet FilterIfAppealPredmetExsists(Guid id)
            => _dbContext.AppealPredmets
            .FirstOrDefault(c => c.Id == id)
                ?? throw new NotFoundException(
                    " There is no AppealPredmet with id. ");

    }

}
