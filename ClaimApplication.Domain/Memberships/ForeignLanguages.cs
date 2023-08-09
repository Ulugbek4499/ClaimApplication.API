using ClaimApplication.Domain.Commons;

namespace ClaimApplication.Domain.Memberships
{
    public class ForeignLanguages : BaseAuditableEntity
    {
        public string Name { get; set; }
        public virtual ICollection<MembershipApplication> MembershipApplications { get; set; }
    }
}