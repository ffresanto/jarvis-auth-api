using JarvisAuth.API.Controllers.Base;
using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Responses.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Swashbuckle.AspNetCore.Annotations;

namespace JarvisAuth.API.Controllers
{
    [ApiController]
    [Route("api/health")]
    [Produces("application/json")]
    public class HealthController(HealthCheckService healthCheckService) : BaseController
    {
        [HttpGet]
        [SwaggerOperation(Summary = "Checks the status and integrity of the API's resources and services")]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION_500, typeof(Response<string[]>))]
        public async Task<ActionResult> GetHealthCheck()
        {
            return Ok(await healthCheckService.CheckHealthAsync());
        }
    }
}
