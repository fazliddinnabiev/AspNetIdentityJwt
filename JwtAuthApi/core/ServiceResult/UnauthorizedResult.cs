using JwtAuthApi.core.Enums;

namespace JwtAuthApi.core.ServiceResult
{
    public class UnauthorizedResult<T> : ServiceResult<T>
    {
        private string _message;

        public UnauthorizedResult(string message)
        {
            _message = message;
        }
        public override ResultTypes ResultType => ResultTypes.Unauthorized;

        public override string Errors => _message;

        public override T Data => default(T);
    }
}
