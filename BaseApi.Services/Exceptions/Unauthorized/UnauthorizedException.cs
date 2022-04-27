using Climapi.Services.Exceptions.BaseExceptions;

namespace Climapi.Services.Exceptions.Unauthorized
{
    public class UnauthorizedException : BaseUnauthorizedException
    {
        public UnauthorizedException(string message, int customCode)
            : base(message, customCode) { }
        public UnauthorizedException() : base() { }
    }
}