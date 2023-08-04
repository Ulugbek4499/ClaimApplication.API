using ClaimApplication.Domain.Commons;

namespace ClaimApplication.Domain.Entities
{
    public class AppealType : BaseAuditableEntity
    {
        public string Name { get; set; } = null!;
        public virtual ICollection<Application> Aplications { get; set; } = new List<Application>();
    }
}