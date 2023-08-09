using AutoMapper;
using ClaimApplication.Application.Commons.Exceptions;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Domain.Memberships;
using MediatR;

namespace ClaimApplication.Application.UseCases.Memberships.Commands.DeleteMembership
{
    public record DeleteMembershipApplicationCommand(int Id) : IRequest;

    public class DeleteMembershipApplicationCommandHandler : IRequestHandler<DeleteMembershipApplicationCommand>
    {
        private IApplicationDbContext _dbContext;
        private IMapper _mapper;

        public DeleteMembershipApplicationCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Handle(DeleteMembershipApplicationCommand request, CancellationToken cancellationToken)
        {
            MembershipApplication membershipApplication = FilterIfMembershipApplicationExsists(request.Id);

            _dbContext.MembershipApplications.Remove(membershipApplication);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private MembershipApplication FilterIfMembershipApplicationExsists(int id)
            => _dbContext.MembershipApplications
            .FirstOrDefault(c => c.Id == id)
                ?? throw new NotFoundException(
                    " There is no MembershipApplication with id. ");

    }
}
