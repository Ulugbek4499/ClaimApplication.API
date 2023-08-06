using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimApplication.Application.Commons.Models
{
    public record ExcelReportResponse(byte[] FileContents, string Option, string FileName);
}
