using iText.Html2pdf;
using iText.Html2pdf.Resolver.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace ClaimApplication.Application.UseCases.Applications.PDF;

public class PdfText
{
    public Paragraph Value { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public TextAlignment Alignment { get; set; } = TextAlignment.LEFT;
    public int Page { get; set; } = 1;
    public VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.BOTTOM;
    public int RadAngle { get; set; }
}

public class PdfHelper
{
    public static string HtmlToPdf(string htmlFilePath, bool isLandscapeOrientation = false)
    {
        string tempPath = "html/";
        var fileInfo = new FileInfo(htmlFilePath);
        string pdfFileName = $"{tempPath}{Guid.NewGuid()}.pdf";

        using (var htmlStream = File.Open(htmlFilePath, FileMode.Open, FileAccess.Read))
        {
            var writerProperties = new WriterProperties();

            using (var pdfWriter = new PdfWriter(pdfFileName, writerProperties))
            {
                var pdfDocument = new PdfDocument(pdfWriter);
                var pageSize = PageSize.A4;

                if (isLandscapeOrientation)
                    pageSize = pageSize.Rotate();

                var documentConvertProperties = new ConverterProperties();

                DefaultFontProvider fontProvider = new DefaultFontProvider(true, true, true, "Times New Roman");

                string fontDir = System.IO.Path.Combine("appdata", "staticfiles", "fonts");

                if (Directory.Exists(fontDir))
                    fontProvider.AddDirectory(fontDir);

                documentConvertProperties.SetFontProvider(fontProvider);

                pdfDocument.SetDefaultPageSize(pageSize);

                #region Convert to html
                IList<IElement> elements = HtmlConverter.ConvertToElements(htmlStream, documentConvertProperties);
                pdfDocument.SetTagged();

                Document doc = new Document(pdfDocument, pageSize);

                foreach (IElement element in elements)
                    doc.Add((IBlockElement)element);
                doc.Close();
                #endregion
            }
        }

        return pdfFileName;
    }

    public static void AddTextToPdf(Stream source, Stream destination, PdfText text)
    {
        AddTextToPdf(
            source,
            destination,
            text == null ? null : new PdfText[] { text });
    }

    public static void AddTextToPdf(Stream source, Stream destination, IEnumerable<PdfText> texts)
    {
        using (var pdfReader = new PdfReader(source))
        {
            using (var ms = new MemoryStream())
            {
                using (var pdfWriter = new PdfWriter(ms))
                {
                    var pdfDocument = new PdfDocument(pdfReader, pdfWriter);
                    var pageSize = PageSize.A4;
                    pdfDocument.SetDefaultPageSize(pageSize);
                    pdfDocument.SetTagged();
                    Document doc = new Document(pdfDocument);

                    if (texts != null)
                        foreach (var text in texts)
                            doc.ShowTextAligned(text.Value, text.X, text.Y, text.Page, text.Alignment, text.VerticalAlignment, text.RadAngle);

                    doc.Close();
                    var bytes = ms.ToArray();
                    destination.Write(bytes, 0, bytes.Length);
                }
            }
        }
    }

    public byte[] ReturnReadyPdf(string htmlString)
    {
        //string tempPath = "appdata/tempfiles/";
        //string tempHtmlFileName = $"{tempPath}{Guid.NewGuid()}.html";
        //File.WriteAllText(tempHtmlFileName, htmlString);
        //string pdfFileName = "";
        //pdfFileName = PdfHelper.HtmlToPdf(tempHtmlFileName, false);
        //byte[] bytes = File.ReadAllBytes(pdfFileName);
        //File.Delete(tempHtmlFileName);
        //File.Delete(pdfFileName);
        //return bytes;

        var workStream = new MemoryStream();

        using (var pdfWriter = new PdfWriter(workStream))
        {
            pdfWriter.SetCloseStream(false);

            var pdfDocument = new PdfDocument(pdfWriter);

            var documentConvertProperties = new ConverterProperties();

            using (var document = HtmlConverter.ConvertToDocument(htmlString, pdfDocument, documentConvertProperties))
            {
                document.SetBottomMargin(0);
                document.SetTopMargin(0);
                document.Flush();
            }
        }

        workStream.Position = 0;
        return workStream.ToArray();
    }

    public string ReturnReadyHtmlAsString(string filename, Dictionary<string, string> data, Dictionary<string, 
                                           Dictionary<string, List<string>>> tableData, string lang = null)
    {
        var html = GetFileFromStorage(filename, lang);
        string htmlStream = html.ReadAsString();

        if (tableData != null)
            foreach (var oneShablon in tableData)
            {
                int begin = htmlStream.IndexOf($"<!--$start-array-{oneShablon.Key}-->");
                int end = htmlStream.IndexOf($"<!--$end-array-{oneShablon.Key}-->") + $"<!--$end-array-{oneShablon.Key}-->".Length;
                string shablon = htmlStream.Substring(begin, end - begin);
                htmlStream = htmlStream.Replace(shablon, FillTable(shablon, oneShablon.Value));
            }

        if (data != null)
            foreach (var (key, value) in data)
                if (htmlStream.Contains(key))
                    htmlStream = htmlStream.Replace(key, value);
                else
                    htmlStream = htmlStream.Replace(key, "");

        return htmlStream;
    }

    public string GetHtmlTemplate(PrtnContractDto dto)
    {
        Domain.Entities.Application application= new Domain.Entities.Application();
        var inn = application.Inn;
        var nameOfBussiness = application.NameOfBussiness;
        var appealNumber = application.AppealNumber;
        var appealDate = application.AppealDate;
        var membershipAgreementNumber =application.MembershipAgreementNumber;
        var membershipAgreementDate = application.MembershipAgreementDate;



        var contractor = _unitOfWork.ContractorRepository.ById<ContractorListDto>(dto.ContractorId);
        var contractorBank = _unitOfWork.BankRepository.ById<BankListDto>(contractor.BankId ?? 0);
        var contractorSettlementAccount = _unitOfWork.Context.Set<ContractorSettlementAccount>().Where(a => a.OwnerId == contractor.Id).Select(a => a.AccountCode).FirstOrDefault();
        var organization = _unitOfWork.OrganizationRepository.ById<OrganizationListDto>(dto.OrganizationId);
        var organizationSettlementAccount = _unitOfWork.Context.Set<OrganizationSettlementAccount>().Where(a => a.OrganizationId == organization.Id).Select(a => new { a.AccountCode, a.BankId }).FirstOrDefault();
        var organizationBank = _unitOfWork.BankRepository.ById<BankListDto>(organizationSettlementAccount?.BankId ?? 0);
        var contractPosition1 = _unitOfWork.Context.Set<OrganizationSign>().FirstOrDefault(a => a.OwnerId == organization.Id && a.PrtnContractTypeTable.OrderNumber == 3)?.*//*User?.Person?.*//*ShortName ?? "";
        var contractPosition2 = _unitOfWork.Context.Set<OrganizationSign>().FirstOrDefault(a => a.OwnerId == organization.Id && a.PrtnContractTypeTable.OrderNumber == 2)?.*//*User?.Person?.*//*ShortName ?? "";
        var region = _unitOfWork.RegionRepository.ById<RegionListDto>(contractor.RegionId);

        //var applicationGraph = _unitOfWork.Context.Set<PrtnApplicationGraph>().Where(a => a.OwnerId == dto.PrtnApplicationId).Select(a => new AplicationGraphDto
        //{
        //    Year = a.YearIn,
        //    OwnerId = a.OwnerId,
        //    NewVacanciesCount = a.NewVacanciesCount,
        //    Month = a.MonthIn
        //}).ToList();

        var graphResult = new List<GrapthYear>();

        var applicationGraph = _unitOfWork.Context.Set<PrtnApplicationGraph>()
            .Where(a => a.OwnerId == dto.PrtnApplicationId)
            .ToList();

        var applicationGraphYears = applicationGraph.Select(a => a.YearIn).OrderBy(a => a).Distinct();

        if (applicationGraphYears.Any())
            for (int i = applicationGraphYears.Min(); i <= applicationGraphYears.Max(); i++)
            {
                var graphResultItem = new GrapthYear()
                {
                    YearIn = i,
                    Months = new List<GrapthMonth>()
                };

                for (int j = 1; j <= 12; j++)
                {
                    graphResultItem.Months.Add(new GrapthMonth
                    {
                        MonthIn = j,
                        NewVacanciesCount = applicationGraph.Where(a => a.YearIn == i && a.MonthIn == j).Sum(a => a.NewVacanciesCount)
                    }); ;
                }

                graphResult.Add(graphResultItem);
            }

        Dictionary<string, string> data
            = new Dictionary<string, string>()
            {
               {"${DocDate}",dto.DocOn.ToString(Constants.DATE_FORMAT)},
               {"${ContractorRegion}",contractor.Region},
               {"${ContractorDistrict}",contractor.District},
               {"${ContractorName}",contractor.FullName},
               {"${ContractorDirector}",contractor.Director},
               {"${ContractorInn}",contractor.Inn},
               {"${ContractorBankCode}",contractorBank?.BankCode},
               {"${ContractorBankName}",contractor?.Bank},
               {"${ContractorSettlementAccount}",contractorSettlementAccount},
               {"${OrgRegion}",organization.Region},
               {"${OrgDistrict}",organization.District},
               {"${OrgAdress}",organization.Address},
               {"${OrgBankSettlementAccount}",organizationSettlementAccount?.AccountCode},
               {"${OrgBankCode}",organizationBank?.BankCode},
               {"${OrgBankName}",organizationBank?.BankName},
               {"${OrgInn}",organization.Inn},
               {"${ContractorPosition1}",contractPosition1},
               {"${ContractorPosition2}",contractPosition2},
            };

        //string fileName = "ownership_contract_ministry_AB.html";

        string fileName = "";
        if (dto.PrtnContractTypeId == PrtnContractTypeIdConst._50_100)
            fileName = "ownership_contract_district_AB.html";
        else if (dto.PrtnContractTypeId == PrtnContractTypeIdConst._101_200)
            if (region.Soato == RegionSoatoConst.Karakalpakstan)
                fileName = "ownership_contract_kr_vk_ab.html";
            else
                fileName = "ownership_contract_region_AB.html";
        else if (dto.PrtnContractTypeId == PrtnContractTypeIdConst._201__)
        {
            fileName = "ownership_contract_ministry_AB.html";

        }
        else
        {
            AddError("Неверный тип контракта");
            return null;
        }

        return _baseReportService.ReturnReadyHtmlAsString(filename: fileName,
                                                  data: data,
                                                  tableData: null);
    }

    public byte[] GetPdfTemplate(long applicationId)
    {
        var dto = GetByApplicationId(applicationId);
        return _baseReportService.ReturnReadyPdf(GetHtmlTemplate(dto));
    }
}

