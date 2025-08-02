using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Services;
using WebApplication1.ViewModels;

namespace WebApplication1.Domain
{
    public class ReportProvider
    {
        public ReportProvider() { }

        public async Task<List<ClientReportVM>> GenerateReport(List<IFormFile> files)
        {
            using var guidelineReader = new StreamReader(files.First(f => f.ContentDisposition.Contains("medicalGuidelines.json")).OpenReadStream());
            string guidelineJsonValue = await guidelineReader.ReadToEndAsync();
            var guidelineSet = JsonLoader.LoadJson<Dictionary<string, GuidelineSet>>(guidelineJsonValue)["guidelines"];
            using var clientsReader = new StreamReader(files.First(f => f.ContentDisposition.Contains("clientData.json")).OpenReadStream());
            string clientsJsonValue = await clientsReader.ReadToEndAsync();
            var clientList = JsonLoader.LoadJson<ClientList>(clientsJsonValue);

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
            return clientsReport;
        }
    }
}
