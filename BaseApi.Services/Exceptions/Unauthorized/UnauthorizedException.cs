using Climapi.Core.Entities.Enums;
using Climapi.Services.Exceptions.BaseExceptions;

namespace Climapi.Services.Exceptions.Unauthorized
{
    public class UnauthorizedException : BaseUnauthorizedException
    {
        public UnauthorizedException(string? message = null, int customCode = (int)CustomCodeEnum.Unauthorized)
            : base(message, customCode) { }
        //public UnauthorizedException() : base() { }
    }
}