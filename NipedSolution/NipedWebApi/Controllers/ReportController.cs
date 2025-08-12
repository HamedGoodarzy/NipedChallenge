using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NipedModel;
using NipedWebApi.Domain;
using System.Net;
using WebApplication1.ViewModels;

namespace NipedWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController (ILogger<ClientController> logger, IReportProvider provider) : ControllerBase
    {


        [HttpGet]
        [Route("clientsList")]
        public List<ClientReportTO> GetClinetsReport()
        {
            try
            {
                var result = provider.GetClinetsReport();
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
