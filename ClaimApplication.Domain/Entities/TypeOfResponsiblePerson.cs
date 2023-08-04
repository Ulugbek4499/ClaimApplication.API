using ClaimApplication.Domain.Commons;

namespace ClaimApplication.Domain.Entities
{
    public class TypeOfResponsiblePerson : BaseAuditableEntity
    {
        public string Name { get; set; } = null!;
        public virtual ICollection<ResponsiblePerson> ResponsiblePerson { get; set; } = new List<ResponsiblePerson>();
    }
}
