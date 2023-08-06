using AutoMapper;
using ClaimApplication.Application.Commons.Exceptions;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Domain.Entities;
using MediatR;

namespace ClaimApplication.Application.UseCases.AppealTypes.Commands.DeleteAppealType
{
    public record DeleteAppealTypeCommand(int Id) : IRequest;

    public class DeleteAppealTypeCommandHandler : IRequestHandler<DeleteAppealTypeCommand>
    {
        private IApplicationDbContext _dbContext;
        private IMapper _mapper;

        public DeleteAppealTypeCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Handle(DeleteAppealTypeCommand request, CancellationToken cancellationToken)
        {
            AppealType AppealType = FilterIfAppealTypeExsists(request.Id);

            _dbContext.AppealTypes.Remove(AppealType);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private AppealType FilterIfAppealTypeExsists(int id)
            => _dbContext.AppealTypes
            .FirstOrDefault(c => c.Id == id)
                ?? throw new NotFoundException(
                    " There is no AppealType with id. ");

    }

}
