using JwtAuthApi.core.Enums;
using JwtAuthApi.core.ServiceResult;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthApi.extensions;

public static class ControllerExtension
{
    public static ActionResult FromResult<T>(this ControllerBase controller, BaseResult<T> result)
    {
        switch (result.ResultType)
        {
            case ResultTypes.Ok:
                return controller.Ok(result.Data);
            case ResultTypes.NotFound:
                return controller.NotFound(result.Errors);
            case ResultTypes.Unauthorized:
                return controller.Unauthorized(result.Errors);
            case ResultTypes.BadRequest:
                return controller.BadRequest(result.Errors);
            default:
                throw new Exception("Something went wrong");
        }
    }
}
