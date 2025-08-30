using NipedModel;

namespace WebApplication1.Domain
{
    public class ReportProvider(RestClient restClient) : IReportProvider
    {
        public async Task GenerateGuideline(IFormFile guidelineFile)
        {
            try
            {
                using var guidelineReader = new StreamReader(guidelineFile.OpenReadStream());
                string guidelineJsonValue = await guidelineReader.ReadToEndAsync();
                var result = await restClient.PostAsync<string>("Guideline/register", guidelineJsonValue);
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
                throw;
            }
        }

        public async Task GenerateClients(IFormFile clientsFile)
        {
            try
            {
                using var clientsReader = new StreamReader(clientsFile.OpenReadStream());
                string clientsJsonValue = await clientsReader.ReadToEndAsync();
                var result = await restClient.PostAsync<string>("client/register", clientsJsonValue);
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
                throw;
            }
        }

        public async Task<List<ClientReportTO>> LoadReportV2()
        {
            try
            {
                var result = await restClient.GetAsync<List<ClientReportTO>>("Report/clientsListV2");
                return result ?? new List<ClientReportTO>();
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

        public async Task<List<ClientReportTO>> LoadReportV1()
        {
            try
            {
                var result = await restClient.GetAsync<List<ClientReportTO>>("Report/clientsListV1");
                return result ?? new List<ClientReportTO>();
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
                throw;
            }
        }

    }
    public interface IReportProvider
    {
        public Task GenerateGuideline(IFormFile guidelineFile);
        public Task GenerateClients(IFormFile clientsFile);
        public Task<List<ClientReportTO>> LoadReportV1();
        public Task<List<ClientReportTO>> LoadReportV2();
    }
}
