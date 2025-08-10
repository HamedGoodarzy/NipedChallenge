using NipedModel;
using System.Runtime.CompilerServices;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Services;
using WebApplication1.ViewModels;

namespace WebApplication1.Domain
{
    public class ReportProvider(RestClient restClient) //: IReportProvider
    {

        public async Task<List<ClientReportVM>> GenerateReportOld(List<IFormFile> files)
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
        public async Task<List<ClientReportVM>> GenerateGuideline(IFormFile guidelineFile)
        {
            try
            {
                using var guidelineReader = new StreamReader(guidelineFile.OpenReadStream());
                string guidelineJsonValue = await guidelineReader.ReadToEndAsync();
                var guidelineSet = JsonLoader.LoadJson<Dictionary<string, GuidelineSet>>(guidelineJsonValue)["guidelines"];
                var result = await restClient.PostAsync<List<ClientReportVM>>("Guideline/register", guidelineJsonValue);
                //TODO
                return null;
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
                throw;
            }
        }

        public async Task<List<ClientReportVM>> GenerateClients(IFormFile clientsFile)
        {
            try
            {
                using var clientsReader = new StreamReader(clientsFile.OpenReadStream());
                string clientsJsonValue = await clientsReader.ReadToEndAsync();
                var result = await restClient.PostAsync<List<ClientReportVM>>("client/register", clientsJsonValue);
                //TODO
                return null;
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
                throw;
            }
        }

        public async Task<List<ClientReportTO>> LoadReport()
        {
            try
            {
                var result = await restClient.GetAsync<List<ClientReportTO>>("client/report");
                //TODO
                return result;
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
                throw;
            }
        }

        private void WriteExceptionLog(Exception ex)
        {
            //TODO
        }
    }
    public interface IReportProvider
    {
        Task<List<ClientReportVM>> GenerateReport(List<IFormFile> files);
    }
}
