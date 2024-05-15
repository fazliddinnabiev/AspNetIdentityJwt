using JwtAuthApi.core.Enums;
using JwtAuthApi.core.ServiceResult;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthApi.Controllers
{
    public class ApiController : ControllerBase
    {
        public ActionResult ToActionResult<T>(ServiceResult<T> result)
        {
            switch (result.ResultType)
            {
                case ResultTypes.Ok:
                    return this.Ok(result.Data);
                case ResultTypes.NotFound:
                    return this.NotFound(result.Errors);
                case ResultTypes.Unauthorized:
                    return this.Unauthorized(result.Errors);
                case ResultTypes.BadRequest:
                    return this.BadRequest(result.Errors);
                default:
                    throw new NotImplementedException(
                        $"Could not convert a value {result.ResultType} into instance of {nameof(ActionResult)}. Required mapping is not implemented.");
            }
        }
    }
}
