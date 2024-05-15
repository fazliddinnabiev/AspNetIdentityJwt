using JwtAuthApi.core.Enums;

namespace JwtAuthApi.core.ServiceResult;

public class NotFoundResult<T> : BaseResult<T>
{
    private readonly string _message;
    public NotFoundResult(string message)
    {
        _message = message;
    }
    public override ResultTypes ResultType => ResultTypes.NotFound;

    public override string Errors => _message;

    public override T Data => default(T);
}
