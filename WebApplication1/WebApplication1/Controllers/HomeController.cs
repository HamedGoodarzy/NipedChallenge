using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using WebApplication1.Domain;
using WebApplication1.Models;
using WebApplication1.Utilities;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(List<IFormFile> files)
        {
            var gls = new GuidelineSet();
            //gls.Cholesterol.Add(new CholesterolGuideline());
            var asJson = JsonSerializer.Serialize(gls);

            if (files.Count == 0)
            {
                ViewBag.Warning = "The file has no content.";
                return View();
            }
            using var guidelineReader = new StreamReader(files.First(f => f.ContentDisposition.Contains("medicalGuidelines.json")).OpenReadStream());
            string content = await guidelineReader.ReadToEndAsync();
            var guidelineSet = ProcessGuideLines(content) ?? throw new Exception("Processing guidelines encountered an error!");
            UsageExample(guidelineSet);

            using var clientsReader = new StreamReader(files.First(f => f.ContentDisposition.Contains("clientData.json")).OpenReadStream());

            string clientsJson = await clientsReader.ReadToEndAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var clientList = JsonSerializer.Deserialize<ClientList>(clientsJson, options) ?? throw new Exception("processing clientData encountered an error!");
            var generator = new ReportGenerator(guidelineSet);
            List<ClientReportVM> clientsReport = new List<ClientReportVM>();
            foreach (var client in clientList.Clients)
            {
                ClientReportVM clientReportVM = new ClientReportVM();
                clientReportVM.Client = client;
                var flattenedClient = ClientDataFlattener.Flatten(client.MedicalData);
                var report = generator.GenerateReport(flattenedClient);
                clientReportVM.ReportEntries.AddRange(report);
                clientsReport.Add(clientReportVM);
            }
            ViewBag.ClientsReport = clientsReport;
            ViewBag.Message = "Processed successfully!";
            return View(clientsReport);
        }

        void UsageExample(GuidelineSet guidelines)
        {
            var bloodSugarValue = 98;
            var bloodSugarGuideline = guidelines.BloodSugar;
            var result = RuleEvaluator.EvaluateNumeric(bloodSugarValue, bloodSugarGuideline);
            Console.WriteLine($"blood Sugar Category: {result}"); // → "seriousIssue"

            var stress = "Moderate self-reported stress";
            var stressGuideline = guidelines.StressLevels;
            var stressResult = RuleEvaluator.EvaluateText(stress, stressGuideline);
            Console.WriteLine($"Stress Level: {stressResult}"); // → "needsAttention"
        }

        GuidelineSet? ProcessGuideLines(string json)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var guidelines = JsonSerializer.Deserialize<Dictionary<string, GuidelineSet>>(json, options)?["guidelines"];
            return guidelines;
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
