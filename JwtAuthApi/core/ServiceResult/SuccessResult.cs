using JwtAuthApi.core.Enums;

namespace JwtAuthApi.core.ServiceResult;

public class SuccessResult<T> : BaseResult<T>
{
    private readonly T _data;

    public SuccessResult(T data)
    {
        _data = data;
    }
    public override ResultTypes ResultType => ResultTypes.Ok;

    public override string Errors => string.Empty;

    public override T Data => _data;
}
