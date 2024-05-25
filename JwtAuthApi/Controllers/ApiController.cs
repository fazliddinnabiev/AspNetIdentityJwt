using JwtAuthApi.core.Enums;
using JwtAuthApi.core.ServiceResult;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthApi.Controllers;

/// <summary>
/// Provides a base API controller with methods for handling service results.
/// </summary>
public class ApiController : ControllerBase
{
    /// <summary>
    /// Converts a ServiceResult into an ActionResult based on the ResultType of the ServiceResult.
    /// </summary>
    /// <param name="result">The ServiceResult to convert.</param>
    /// <typeparam name="T">The type of data in the ServiceResult.</typeparam>
    /// <returns>An ActionResult that represents the ServiceResult.</returns>
    /// <exception cref="NotImplementedException">Thrown when the ResultType of the ServiceResult is not handled.</exception>
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
            case ResultTypes.OperationFailed:
                return StatusCode(503, result.Errors);
            default:
                throw new NotImplementedException(
                    $"Could not convert a value {result.ResultType} into instance of {nameof(ActionResult)}. Required mapping is not implemented.");
        }
    }
}