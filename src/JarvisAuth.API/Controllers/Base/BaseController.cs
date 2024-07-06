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
                        errorResponse.Message = GlobalMessages.VALIDATION_ERRORS_422;
                        break;
                    case 401:
                        errorResponse.Message = GlobalMessages.UNAUTHORIZED_ACCESS_401;
                        break;
                    case 403:
                        errorResponse.Message = GlobalMessages.ACCESS_DENIED_403;
                        break;
                    case 404:
                        errorResponse.Message = GlobalMessages.REQUEST_NOT_FOUND_404;
                        break;
                    case 409:
                        errorResponse.Message = GlobalMessages.REQUEST_CONFLICT_409;
                        break;
                    default:
                        errorResponse.Message = GlobalMessages.GLOBAL_EXCEPTION_500;
                        break;
                }

                return StatusCode(response.StatusCode, errorResponse);
            }

            return Ok(response);
        }
    }
}
