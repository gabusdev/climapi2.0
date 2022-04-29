using Climapi.Core.Entities.Enums;
using Climapi.Services.Exceptions.BaseExceptions;

namespace Climapi.Services.Exceptions.BadRequest
{
    public class WrongForeignKeyBadRequestException : BaseBadRequestException
    {
        //public WrongForeignKeyBadRequestException() : base() { }
        public WrongForeignKeyBadRequestException(string? message = null, int customCode = (int)CustomCodeEnum.WrongForeignKey)
            : base(message, customCode) { }
    }
}