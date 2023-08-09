using ClaimApplication.Application.UseCases.Applications.Response;
using ClaimApplication.Application.UseCases.Memberships.Commands.DeleteMembership;
using ClaimApplication.Application.UseCases.Memberships.Queries.GetAllMemberships;
using ClaimApplication.Application.UseCases.Memberships.Queries.GetMembershipById;
using ClaimMembershipApplication.MembershipApplication.UseCases.MembershipApplications.Commands.CreateMembershipApplication;
using Microsoft.AspNetCore.Mvc;
using Xceed.Words.NET;

namespace ClaimApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipApplicationController : BaseApiController
    {
        [HttpPost("[action]")]
        public async ValueTask<int> CreateMembershipApplication(CreateMembershipApplicationCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("[action]")]
        public async ValueTask<MembershipApplicationResponse> GetMembershipApplicationById(int Id)
        {
            return await _mediator.Send(new GetMembershipApplicationByIdQuery(Id));
        }

        [HttpGet("[action]")]
        public async ValueTask<IEnumerable<MembershipApplicationResponse>> GetAllMembershipApplications()
        {
            return (IEnumerable<MembershipApplicationResponse>)await _mediator.Send(
                new GetMembershipApplicationsQuery());
        }

    
        [HttpDelete("[action]")]
        public async ValueTask<IActionResult> DeleteMembershipApplication(DeleteMembershipApplicationCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GenerateDocxAsync(int id)
        {
            
            MembershipApplicationResponse ma = await _mediator.Send(new GetMembershipApplicationByIdQuery(id));
            var templatePath = @"D:\PDP\COMPANIES\ClaimApplication.API\ClaimApplication.API\wwwroot\docx\Anketa.docx";

            var doc = DocX.Load(templatePath);

            doc.ReplaceText("%NameOfBusiness%", ma.NameOfBusiness);
            doc.ReplaceText("%FullNameOfManager%", ma.FullNameOfManager);
            doc.ReplaceText("%Gender%", ma.Gender.Value.ToString());
            doc.ReplaceText("%Address%", ma.Address);
            doc.ReplaceText("%PostIndex%", ma.PostIndex);

            doc.ReplaceText("%PhoneNumber%", ma.PhoneNumber);
            doc.ReplaceText("%MobileNumberFirst%", ma.MobileNumberFirst);
            doc.ReplaceText("%MobileNumberSecond%", ma.MobileNumberSecond);
            doc.ReplaceText("%Fax%", ma.Fax);
            doc.ReplaceText("%MobileNumberExtra%", ma.MobileNumberExtra);

            doc.ReplaceText("%EmailFirst%", ma.EmailFirst);
            doc.ReplaceText("%EmailSecond%", ma.EmailSecond);
            doc.ReplaceText("%WebSite%", ma.WebSite);
            doc.ReplaceText("%SkypeProfile%", ma.SkypeProfile);
            doc.ReplaceText("%FaceBookProfile%", ma.FaceBookProfile);

            doc.ReplaceText("%TelegramProfile%", ma.TelegramProfile);
            doc.ReplaceText("%ExtraProfile%", ma.ExtraProfile);
            doc.ReplaceText("%BussinessRegesteredDate%", ma.BussinessRegesteredDate.ToString());
            doc.ReplaceText("%RegistrationNumber%", ma.RegistrationNumber);
            doc.ReplaceText("%HasCopyOfRegistrationCerteficate%", ma.HasCopyOfRegistrationCerteficate.Value.ToString());

            doc.ReplaceText("%Inn%", ma.Inn);
            doc.ReplaceText("%OKED%", ma.OKED);
            doc.ReplaceText("%NumberOfEmployees%", ma.NumberOfEmployees.ToString());
            doc.ReplaceText("%NameOfBank%", ma.NameOfBank);
            doc.ReplaceText("%CodeOfBank%", ma.CodeOfBank.ToString());

            doc.ReplaceText("%BankAccount%", ma.BankAccount);
            doc.ReplaceText("%AnnualTurnoverOfEnterprise%", ma.AnnualTurnoverOfEnterprise.ToString());
            doc.ReplaceText("%AnnualPaidTax%", ma.AnnualPaidTax.ToString());
            doc.ReplaceText("%AnnualExportAmount%", ma.AnnualExportAmount.ToString());
            doc.ReplaceText("%AnnualImportAmount%", ma.AnnualImportAmount.ToString());
            doc.ReplaceText("%AnnualProductionAmount%", ma.AnnualProductionAmount.ToString());

            doc.ReplaceText("%BrandName%", ma.BrandName);
            doc.ReplaceText("%BithDateOfManager%", ma.BithDateOfManager);
            doc.ReplaceText("%SeriesOfPassport%", ma.SeriesOfPassport);
            doc.ReplaceText("%NumberOfPassport%", ma.NumberOfPassport);

            doc.ReplaceText("%PassportGivenFrom%", ma.PassportGivenFrom);
            doc.ReplaceText("%Nationality%", ma.Nationality);
            doc.ReplaceText("%EducationDegree%", ma.EducationDegree);

            doc.ReplaceText("%ExtraInformation%", ma.ExtraInformation);
            doc.ReplaceText("%Gender%", ma.Gender.ToString());
            doc.ReplaceText("%ForeignLanguage%", ma.ForeignLanguage.ToString());

            doc.ReplaceText("%MainActivity%", ma.MainActivity.ToString());
            doc.ReplaceText("%BussinessCategory%", ma.BussinessCategory.ToString());

            var newFilename = $"output_{ma.NameOfBusiness}.docx";

            doc.SaveAs(newFilename);

            var fileBytes = System.IO.File.ReadAllBytes(newFilename);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", newFilename);
        }
        
    }
}
