using JwtAuthApi.core.Enums;

namespace JwtAuthApi.core.ServiceResult;

public class BadRequestResult<T> : ServiceResult<T>
{
    private string _message;
    public BadRequestResult(string message)
    {
        _message = message;
    }
    public override ResultTypes ResultType => ResultTypes.BadRequest;

    public override string Errors => _message;

    public override T Data => default(T);
}
