using JarvisAuth.Core.Messages;
using JarvisAuth.Core.Responses.Shared;
using Microsoft.AspNetCore.Mvc;

namespace JarvisAuth.API.Controllers.Base
{
    public abstract class BaseController : ControllerBase
    {
        public ActionResult CustomResponse<T>(Response<T> response)
        {
            if (response.Errors.Count > 0)
            {
                var errorResponse = new Response<string[]> { StatusCode = response.StatusCode, Success = false, Errors = response.Errors, Data = [] };

                switch (errorResponse.StatusCode)
                {
                    case 422:
                        errorResponse.Message = GlobalMessages.OPERATION_VALIDATIONS_ERROS;
                        break;
                    case 404:
                        errorResponse.Message = GlobalMessages.OPERATION_REQUEST_NOT_FOUND;
                        break;
                    default:
                        errorResponse.Message = GlobalMessages.OPERATION_FAILED;
                        break;
                }

                return StatusCode(response.StatusCode, errorResponse);
            }

            return Ok(response);
        }
    }
}
