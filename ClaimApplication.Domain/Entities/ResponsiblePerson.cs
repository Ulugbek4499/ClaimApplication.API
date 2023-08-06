using ClaimApplication.Domain.Commons;

namespace ClaimApplication.Domain.Entities
{
    public class ResponsiblePerson : BaseAuditableEntity
    {
        public string OrdinalNumber { get; set; } = null!;
        public string Inn { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

        public int ApplicationId { get; set; }
        public virtual Application Application { get; set; } = null!;

        public int TypeOfResponsiblePersonId { get; set; }
        public virtual TypeOfResponsiblePerson TypeOfResponsiblePerson { get; set; } = null!;
    }
}
