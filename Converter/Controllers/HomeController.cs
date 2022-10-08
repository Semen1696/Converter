
using Converter.Services.Files;
using Microsoft.AspNetCore.Mvc;
using PuppeteerSharp;
using System.Diagnostics;

namespace Converter.Controllers
{
    public class HomeController : Controller
    {      
        private readonly IFileService _fileService;
        public HomeController(IFileService fileService)
        {          
            _fileService = fileService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConvertFile(IFormFile file)
        {
            if (!file.ContentType.Contains("html"))
                return StatusCode(400, "File is not of html type");

            var content = await _fileService.GetHtmlContent(file);
            var result = await _fileService.ConvertHtmlToPdf(content);
            if(result.Error != null)
                return Json(new { success = false, message = result.Error, status = 500 });

            return Json(new { success = true, message = result.Path, status = 200 });
        }

        [HttpPost]
        public async Task<IActionResult> DownloadFile(string path)
        {
            var bytes = await System.IO.File.ReadAllBytesAsync(path);
            //var stream = new MemoryStream(bytes);
            //var result = new FileStreamResult(stream, "application/pdf");
            //result.FileDownloadName = Path.GetFileName(path);
            return File(bytes, "application/pdf");
        }

    }
}