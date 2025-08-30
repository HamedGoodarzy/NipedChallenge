using Microsoft.AspNetCore.Mvc;
using NipedModel;
using NipedWebApi.Domain;

namespace NipedWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController (ILogger<ClientController> logger, IReportProvider provider) : ControllerBase
    {
        [HttpGet]
        [Route("clientsListV2")]
        public List<ClientReportTO> GetClinetsReportV2()
        {
            try
            {
                var result = provider.GetClinetsReportV2();
                logger.LogInformation("Loaded clients report at {Time}", DateTime.UtcNow);
                return result;
            }
            //TODO

            catch (ArgumentException aex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("clientsListV1")]
        public List<ClientReportTO> GetClinetsReportV1()
        {
            try
            {
                var result = provider.GetClinetsReportV1();
                return result;
            }
            //TODO

            catch (ArgumentException aex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }



    }
}
