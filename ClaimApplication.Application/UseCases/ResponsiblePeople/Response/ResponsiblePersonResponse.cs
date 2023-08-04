namespace ClaimApplication.Application.UseCases.ResponsiblePeople.Response
{
    public class ResponsiblePersonResponse
    {
        public Guid Id { get; set; }
        public string OrdinalNumber { get; set; } = null!;
        public string Inn { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

        public Guid ApplicationId { get; set; }
        public Guid TypeOfResponsiblePersonId { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifyBy { get; set; }
    }
}