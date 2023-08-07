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

public static class ExtentionMethod
{
    public static string ReadAsString(this FileInfo fileInfo)
    {
        using (StreamReader reader = fileInfo.OpenText())
        {
            return reader.ReadToEnd();
        }
    }
}

