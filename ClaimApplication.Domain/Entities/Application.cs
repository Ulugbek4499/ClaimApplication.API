using ClaimApplication.Domain.Commons;

namespace ClaimApplication.Domain.Entities
{
    public class Application : BaseAuditableEntity
    {
        public string Inn { get; set; }
        public string NameOfBussiness { get; set; }
        public int AppealNumber { get; set; }
        public DateTime AppealDate { get; set; }
        public string MembershipAgreementNumber { get; set; }
        public DateTime MembershipAgreementDate { get; set; }
        public string CertificateNumber { get; set; } = null!;
        public DateTime CertificateGivenDate { get; set; }
        public string? PreviousAppeal { get; set; }  
        public string AppealText { get; set; }
        public decimal? TotalClaimAmount { get; set; }
        public decimal? MainDebt { get; set; }
        public decimal? CalculatedLateCharges { get; set; }
        public decimal? AmountOfFine { get; set; }
        public decimal? Percentage { get; set; }

        public Guid AppealPredmetId { get; set; }
        public virtual AppealPredmet AppealPredmet { get; set; }
        
        public Guid AppealTypeId { get; set; }
        public AppealType AppealType { get; set; }

        public virtual ICollection<ResponsiblePerson> ResponsiblePeople { get; set; } = new List<ResponsiblePerson>();
    }
}
