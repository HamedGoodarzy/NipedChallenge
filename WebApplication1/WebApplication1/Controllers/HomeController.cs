using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using WebApplication1.Domain;
using WebApplication1.Models;
using WebApplication1.Utilities;

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
            //var firstClient = clientList.Clients.First();
            //var flattenedClient = ClientDataFlattener.Flatten(firstClient.MedicalData);

            //foreach (var client in clientList.Clients)
            //{
            //    var data = ClientDataFlattener.Flatten(client.MedicalData);
            //    Console.WriteLine($"--- {client.Name} ---");
            //    foreach (var item in data)
            //        Console.WriteLine($"{item.Key}: {item.Value}");
            //}

            //var generator = new ReportGenerator(guidelineSet);
            //var report = generator.GenerateReport(flattenedClient);

            var generator = new ReportGenerator(guidelineSet);
            foreach (var client in clientList.Clients)
            {
                Console.WriteLine($"Report for {client.Name}:");
                var flattenedBloodWork = ClientDataFlattener.Flatten(client.MedicalData);
                //Console.WriteLine($"BloodWork:");
                var report = generator.GenerateReport(flattenedBloodWork);
                foreach (var entry in report)
                {
                    Console.WriteLine($"{entry.MetricPath}: {entry.Value} -> {entry.Category}");
                }
                //var flattenedQuestionnaire = ClientDataFlattener.Flatten(client.MedicalData.Questionnaire);
                //Console.WriteLine($"Questionnaire:");
                //report = generator.GenerateReport(flattenedBloodWork);
                //foreach (var entry in report)
                //{
                    //Console.WriteLine($"{entry.MetricPath}: {entry.Value} -> {entry.Category}");
                //}
            }

            ViewBag.Message = "Processed successfully!";
            return View();
        }

        //void F1()
        //{
        //var flattened = ClientDataFlattener.Flatten(client.MedicalData);

        //foreach (var kv in flattened)
        //{
        //Console.WriteLine($"{kv.Key} = {kv.Value}");
        //}
        //}
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
