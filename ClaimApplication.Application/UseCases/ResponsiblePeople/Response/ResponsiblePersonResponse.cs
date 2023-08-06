namespace ClaimApplication.Application.UseCases.ResponsiblePeople.Response
{
    public class ResponsiblePersonResponse
    {
        public int Id { get; set; }
        public string OrdinalNumber { get; set; } = null!;
        public string Inn { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

        public int ApplicationId { get; set; }
        public int TypeOfResponsiblePersonId { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}