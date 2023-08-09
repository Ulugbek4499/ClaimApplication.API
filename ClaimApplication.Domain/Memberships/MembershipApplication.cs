using ClaimApplication.Domain.Commons;

namespace ClaimApplication.Domain.Memberships
{
    public class MembershipApplication : BaseAuditableEntity
    {
        public string? NameOfBusiness { get; set; }
        public string? FullNameOfManager { get; set; }
        public Gender? Gender { get; set; }
        public string? Address { get; set; }
        public string? PostIndex { get; set; }
        public string? PhoneNumber { get; set; }
        public string? MobileNumberFirst { get; set; }
        public string? MobileNumberSecond { get; set; }
        public string? Fax { get; set; }
        public string? MobileNumberExtra { get; set; }
        public string? EmailFirst { get; set; }
        public string? EmailSecond { get; set; }
        public string? WebSite { get; set; }
        public string? SkypeProfile { get; set; }
        public string? FaceBookProfile { get; set; }
        public string? TelegramProfile { get; set; }
        public string? ExtraProfile { get; set; }
        public DateTime? BussinessRegesteredDate { get; set; }
        public string? RegistrationNumber { get; set; }
        public bool? HasCopyOfRegistrationCerteficate { get; set; }
        public string? Inn { get; set; }
        public string? OKED { get; set; }

        public int? MainActivityId { get; set; }
        public MainActivityType? MainActivityType { get; set; }

        public int? BussinessCategoryId { get; set; }
        public BussinessCategory? BussinessCategory { get; set; }

        public int? NumberOfEmployees { get; set; }
        public string? NameOfBank { get; set; }
        public string? CodeOfBank { get; set; }
        public string? BankAccount { get; set; }
        public decimal? AnnualTurnoverOfEnterprise { get; set; }
        public decimal? AnnualPaidTax { get; set; }
        public decimal? AnnualExportAmount { get; set; }
        public decimal? AnnualImportAmount { get; set; }
        public decimal? AnnualProductionAmount { get; set; }
        public string? BrandName { get; set; }
        public string? BithDateOfManager { get; set; }
        public string? SeriesOfPassport { get; set; }
        public string? NumberOfPassport { get; set; }
        public string? PassportGivenFrom { get; set; }
        public string? Nationality { get; set; }

        public int? ForeignLanguageId { get; set; }
        public ForeignLanguages? ForeignLanguages  { get; set; }

        public string? EducationDegree { get; set; }
        public string? ExtraInformation { get; set; }
    }
}
