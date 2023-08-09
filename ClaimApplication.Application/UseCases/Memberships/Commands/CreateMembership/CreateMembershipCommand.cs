using ClaimApplication.Domain.Commons;

namespace ClaimApplication.Application.UseCases.Memberships.Commands.CreateMembership
{
    public class CreateMembershipCommand:BaseAuditableEntity
    {
        public string Name { get; set; }
    }
}
