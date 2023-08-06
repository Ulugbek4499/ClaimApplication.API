namespace ClaimApplication.Application.Commons.Models
{
    public record ExcelReportResponse(byte[] FileContents, string Option, string FileName);
}
