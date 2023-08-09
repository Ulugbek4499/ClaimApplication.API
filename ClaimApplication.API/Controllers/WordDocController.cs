﻿using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Office.Interop.Word;

namespace ClaimApplication.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WordDocController : BaseApiController
{
    [HttpPost("[action]")]
    public async Task<IActionResult> WordToBase64(IFormFile wordFile)
    {
        if (wordFile == null || wordFile.Length <= 0)
        {
            return BadRequest("No file uploaded or the file is empty.");
        }

        using (var memoryStream = new MemoryStream())
        {
            await wordFile.CopyToAsync(memoryStream);
            byte[] fileBytes = memoryStream.ToArray();
            string base64String = Convert.ToBase64String(fileBytes);

            return Ok(base64String);
        }
    }


    [HttpPost("[action]")]
    public IActionResult Base64ToWord([FromBody] string base64Data)
    {
        if (string.IsNullOrWhiteSpace(base64Data))
        {
            return BadRequest("Base64 data is missing.");
        }

        try
        {
            byte[] documentBytes = Convert.FromBase64String(base64Data);

            string contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

            string fileName = "document.docx";

            return File(documentBytes, contentType, fileName);
        }
        catch (FormatException)
        {
            return BadRequest("Invalid Base64 data.");
        }
    }

    [HttpPost("[action]")]
    public IActionResult Base64ToPdf([FromBody] string base64Data)
    {
        if (string.IsNullOrWhiteSpace(base64Data))
        {
            return BadRequest("Base64 data is missing.");
        }

        try
        {
            byte[] pdfBytes = Convert.FromBase64String(base64Data);

            string contentType = "application/pdf";
            string fileName = "document.pdf";

            return File(pdfBytes, contentType, fileName);
        }
        catch (FormatException)
        {
            return BadRequest("Invalid Base64 data.");
        }
    }
}









