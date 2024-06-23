using JarvisAuth.API.Controllers.Base;
using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Requests.Jarvis;
using JarvisAuth.Core.Responses.Jarvis;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JarvisAuth.API.Controllers
{
    [ApiController]
    [Route("api/jarvis")]
    [Produces("application/json")]
    public class JarvisController(IJarvisService jarvisService) : BaseController
    {
        [HttpPost("users/create")]
        [SwaggerOperation(Summary = "The route creates a user for the Jarvis Auth.")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCESSS, typeof(Response<string>))]
        [SwaggerResponse(422, GlobalMessages.OPERATION_VALIDATIONS_ERROS, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION, typeof(Response<string>))]
        public async Task<ActionResult> PostCreateUserJarvis(PostCreateUserJarvisRequest request)
        {
            return CustomResponse(await jarvisService.PostCreateUserJarvis(request));
        }
        [HttpGet("types/genders")]
        [SwaggerOperation(Summary = "Return lista of gender types")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCESSS, typeof(Response<List<GetGenderTypeResponse>>))]
        [SwaggerResponse(404, GlobalMessages.OPERATION_REQUEST_NOT_FOUND, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION, typeof(Response<string>))]
        public async Task<ActionResult> GetGendersTypes()
        {
            return CustomResponse(await jarvisService.GetGendersTypes());
        }

        [HttpGet("types/documents")]
        [SwaggerOperation(Summary = "Return list of document types")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCESSS, typeof(Response<List<GetDocumentTypeResponse>>))]
        [SwaggerResponse(404, GlobalMessages.OPERATION_REQUEST_NOT_FOUND, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION, typeof(Response<string>))]
        public async Task<ActionResult> GetDocumentsTypes()
        {
            return CustomResponse(await jarvisService.GetDocumentsTypes());
        }
    }
}
