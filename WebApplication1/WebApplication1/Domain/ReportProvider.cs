using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Services;
using WebApplication1.ViewModels;

namespace WebApplication1.Domain
{
    public class ReportProvider : IReportProvider
    {
        public async Task<List<ClientReportVM>> GenerateReport(List<IFormFile> files)
        {
            using var guidelineReader = new StreamReader(files.First(f => f.ContentDisposition.Contains("medicalGuidelines.json")).OpenReadStream());
            string guidelineJsonValue = await guidelineReader.ReadToEndAsync();
            var guidelineSet = JsonLoader.LoadJson<Dictionary<string, GuidelineSet>>(guidelineJsonValue)["guidelines"];
            using var clientsReader = new StreamReader(files.First(f => f.ContentDisposition.Contains("clientData.json")).OpenReadStream());
            string clientsJsonValue = await clientsReader.ReadToEndAsync();
            var clientList = JsonLoader.LoadJson<ClientList>(clientsJsonValue);

            IRuleEvaluator ruleEvaluator = new RuleEvaluator();
            IReportGenerator reportGenerator = new ReportGenerator(guidelineSet, ruleEvaluator);
            List<ClientReportVM> clientsReportVM = new List<ClientReportVM>();
            foreach (var client in clientList.Clients)
            {
                var flattenedClient = ClientDataFlattener.Flatten(client.MedicalData);
                clientsReportVM.Add(new ClientReportVM()
                {
                    Client = client,
                    ReportEntries = reportGenerator.GenerateReport(flattenedClient),
                });
            }
            return clientsReportVM;
        }
    }
    interface IReportProvider
    {
        Task<List<ClientReportVM>> GenerateReport(List<IFormFile> files);
    }
}
