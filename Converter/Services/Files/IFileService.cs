using Converter.Models;

namespace Converter.Services.Files
{
    public interface IFileService
    {
        Task<string> GetHtmlContent(IFormFile file);
        Task<ConverterResult> ConvertHtmlToPdf(string html);
    }
}
