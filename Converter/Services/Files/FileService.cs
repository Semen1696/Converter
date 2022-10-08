using Converter.Models;
using PuppeteerSharp;

namespace Converter.Services.Files
{
    public class FileService : IFileService
    {
        public async Task<string> GetHtmlContent(IFormFile file)
        {
            var f = file.OpenReadStream();
            using var sr = new StreamReader(f);
            string content = await sr.ReadToEndAsync();
            return content;
        }

        public async Task<ConverterResult> ConvertHtmlToPdf(string html)
        {
            try
            {
                var browserFetcher = new BrowserFetcher();
                await browserFetcher.DownloadAsync();
                await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
                await using var page = await browser.NewPageAsync();
                await page.SetContentAsync(html);

                var dir = Environment.CurrentDirectory + @"output.pdf";
                await page.PdfAsync(dir);
                return new ConverterResult
                {
                    Path = dir
                };
            }
            catch(Exception ex)
            {
                return new ConverterResult { Error = ex.Message };
            }
         
        }
    }
}
