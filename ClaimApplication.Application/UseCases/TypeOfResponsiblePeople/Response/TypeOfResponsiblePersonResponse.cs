namespace ClaimApplication.Application.UseCases.TypeOfResponsiblePeople.Response
{
    public class TypeOfResponsiblePersonResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifyBy { get; set; }
    }
}