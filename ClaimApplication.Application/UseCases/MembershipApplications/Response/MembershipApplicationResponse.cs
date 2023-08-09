using ClaimApplication.Application.UseCases.ResponsiblePeople.Response;

namespace ClaimApplication.Application.UseCases.Applications.Response
{
    public class MembershipApplicationResponse
    {
        public int Id { get; set; }
  



        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }

        //  public virtual ICollection<ResponsiblePersonResponse> ResponsiblePeople { get; set; } = new List<ResponsiblePersonResponse>();
    }
}
