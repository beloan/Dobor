using Application.Abstractions.ServiceAbstractions;
using Aspose.Words;     

namespace Infrastructure.Services
{
    public class HtmlToDocConverter : IHtmlToDocConverter
    {
        public byte[]? Convert(string html)
        {
            Document document = new Document();

            DocumentBuilder builder = new DocumentBuilder(document);
            document.Watermark.Remove();

            builder.InsertHtml(html);

            var stream = new MemoryStream();

            document.Save(stream, SaveFormat.Docx);

            return stream.ToArray();
        }
    }
}
