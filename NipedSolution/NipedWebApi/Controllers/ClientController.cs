using Microsoft.AspNetCore.Mvc;
using NipedWebApi.Domain;

namespace NipedWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController (ILogger<ClientController> logger, IClientProvider provider) : ControllerBase
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
    }
}
