using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Domain;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IReportProvider _reportProvider;
        public HomeController(ILogger<HomeController> logger, IReportProvider reportProvider)
        {
            _logger = logger;
            _reportProvider = reportProvider;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Index(List<IFormFile> files)
        //{
        //    if (files.Count == 0)
        //    {
        //        ViewBag.Warning = "The file has no content.";
        //        return View();
        //    }
            
        //    ViewBag.ClientsReport = await _reportProvider.GenerateReportOld(files);
        //    ViewBag.Message = "Processed successfully!";
        //    return View();
        //}

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file, string type)
        {
            if (type == "loadReport")
            {
                var result = await _reportProvider.LoadReport();
                ViewBag.ClientsReport = result;
                return View();
            }
            if (file == null)
            {
                ViewBag.Warning = "The file has no content.";
                return View();
            }
            if (type == "guideline")
            {
                await _reportProvider.GenerateGuideline(file);
                ViewBag.Message = "Guideline Processed successfully!";
            }
            else if (type == "clientsData")
            {
                await _reportProvider.GenerateClients(file);
                ViewBag.Message = "Clients Data Processed successfully!";
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
