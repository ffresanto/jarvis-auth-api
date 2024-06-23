using JarvisAuth.API.Controllers.Base;
using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Requests.System;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Core.Responses.Types;
using JarvisAuth.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JarvisAuth.API.Controllers
{
    [ApiController]
    [Route("api/system")]
    [Produces("application/json")]
    public class SystemController(ISystemService typeService) : BaseController
    {
        [HttpPost("create-user-system")]
        [SwaggerOperation(Summary = "The route creates a user for the Jarvis Auth.")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCESSS, typeof(Response<string>))]
        [SwaggerResponse(422, GlobalMessages.OPERATION_VALIDATIONS_ERROS, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION, typeof(Response<string>))]
        public async Task<ActionResult> PostCreateUserSystem(PostCreateUserSystemRequest request)
        {
            return CustomResponse(await typeService.PostCreateUserSystem(request));
        }
        [HttpGet("genders-types")]
        [SwaggerOperation(Summary = "Return lista of gender types")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCESSS, typeof(Response<List<GetGenderTypeResponse>>))]
        [SwaggerResponse(404, GlobalMessages.OPERATION_REQUEST_NOT_FOUND, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION, typeof(Response<string>))]
        public async Task<ActionResult> GetGendersTypes()
        {
            return CustomResponse(await typeService.GetGendersTypes());
        }

        [HttpGet("documents-types")]
        [SwaggerOperation(Summary = "Return list of document types")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCESSS, typeof(Response<List<GetDocumentTypeResponse>>))]
        [SwaggerResponse(404, GlobalMessages.OPERATION_REQUEST_NOT_FOUND, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION, typeof(Response<string>))]
        public async Task<ActionResult> GetDocumentsTypes()
        {
            return CustomResponse(await typeService.GetDocumentsTypes());
        }
    }
}
