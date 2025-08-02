using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using WebApplication1.Domain;
using WebApplication1.Models;

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
        public async Task<IActionResult> Index(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ViewBag.Warning = "The file has no content.";
                return View();
            }
            using var reader = new StreamReader(file.OpenReadStream());
            string content = await reader.ReadToEndAsync();
            var guidelineSet = ProcessGuideLines(content) ?? throw new Exception("Processing guidelines encountered an error!");
            UsageExample(guidelineSet);
            ViewBag.Message = "Processed successfully!";
            return View();
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
