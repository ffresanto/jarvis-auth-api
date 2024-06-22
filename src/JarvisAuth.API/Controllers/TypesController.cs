using JarvisAuth.API.Controllers.Base;
using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Core.Responses.Types;
using JarvisAuth.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JarvisAuth.API.Controllers
{
    [ApiController]
    [Route("api/types")]
    [Produces("application/json")]
    public class TypesController(ITypeService typeService) : BaseController
    {
        [HttpGet("genders")]
        [SwaggerOperation(Summary = "Return lista of gender types")]
        [SwaggerResponse(200, GlobalMessages.OPERATION_SUCESSS, typeof(Response<List<GetGenderTypeResponse>>))]
        [SwaggerResponse(404, GlobalMessages.OPERATION_REQUEST_NOT_FOUND, typeof(Response<string>))]
        [SwaggerResponse(500, GlobalMessages.GLOBAL_EXCEPTION, typeof(Response<string>))]
        public async Task<ActionResult> GetGendersTypes()
        {
            return CustomResponse(await typeService.GetGendersTypes());
        }

        [HttpGet("documents")]
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
