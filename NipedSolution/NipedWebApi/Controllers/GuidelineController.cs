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
    public class GuidelineController (ILogger<GuidelineController> logger, IBaseInfoProvider provider) : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public dynamic GetGuideline()
        {
            try
            {
                return provider.GetGuideline();
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

        [HttpPost]
        [Route("register")]
        public string RegisterGuideline([FromBody] string guidelineAsJson)
        {
            try
            {
                return provider.RegisterGuideline(guidelineAsJson);
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
