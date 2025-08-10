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
    public class ClientController (ILogger<ClientController> logger, Provider provider) : ControllerBase
    {
        [HttpPost]
        [Route("register")]
        public string RegisterClientList([FromBody] string clientsAsJson)
        {
            try
            {
                return provider.RegisterClientList(clientsAsJson);
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
        [Route("report")]
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
