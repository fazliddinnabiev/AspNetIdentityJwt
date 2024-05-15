using JwtAuthApi.core.Enums;

namespace JwtAuthApi.core.ServiceResult;

public abstract class ServiceResult<T>
{
    public abstract ResultTypes ResultType { get; }
    public abstract string Errors { get; }
    public abstract T Data { get; }
}
