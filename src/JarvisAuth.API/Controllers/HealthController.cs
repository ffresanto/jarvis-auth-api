using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Swashbuckle.AspNetCore.Annotations;

namespace JarvisAuth.API.Controllers
{
    [ApiController]
    [Route("api/health")]
    [Produces("application/json")]
    public class HealthController(HealthCheckService healthCheckService) : Controller
    {
        [HttpGet]
        [SwaggerOperation(Summary = "Checks the status and integrity of the API's resources and services")]
        [SwaggerResponse(500, "An unexpected error occurred while processing your request.", typeof(string))]
        public async Task<ActionResult> GetHealthCheck()
        {
            return Ok(await healthCheckService.CheckHealthAsync());
        }
    }
}
