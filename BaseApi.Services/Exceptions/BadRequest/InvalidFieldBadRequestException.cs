using Climapi.Core.Entities.Enums;
using Climapi.Services.Exceptions.BaseExceptions;

namespace Climapi.Services.Exceptions.BadRequest
{
    public class InvalidFieldBadRequestException : BaseBadRequestException
    {
        //public InvalidFieldBadRequestException() : base() { }
        public InvalidFieldBadRequestException(string? message = null, int customCode = (int)CustomCodeEnum.InvalidField)
            : base(message, customCode) { }
    }
}