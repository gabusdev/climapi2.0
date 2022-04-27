using Climapi.Services.Exceptions.BaseExceptions;

namespace Climapi.Services.Exceptions.BadRequest
{
    public class WrongForeignKeyBadRequestException : BaseBadRequestException
    {
        public WrongForeignKeyBadRequestException() : base() { }
        public WrongForeignKeyBadRequestException(string message, int customCode)
            : base(message, customCode) { }
    }
}