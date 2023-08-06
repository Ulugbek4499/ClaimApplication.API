using ClaimApplication.Application.UseCases.ResponsiblePeople.Response;

namespace ClaimApplication.Application.UseCases.Applications.Response
{
    public class ApplicationResponse
    {
        public int Id { get; set; }
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
        public int AppealPredmetId { get; set; }
        public int AppealTypeId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }

        public virtual ICollection<ResponsiblePersonResponse> ResponsiblePeople { get; set; } = new List<ResponsiblePersonResponse>();
    }
}
