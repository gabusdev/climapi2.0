using Climapi.Core.Entities.Enums;
using Climapi.Services.Exceptions.BaseExceptions;

namespace Climapi.Services.Exceptions.BadRequest
{
    public class DbUpdateFailedBadRequestException : BaseBadRequestException
    {
        public DbUpdateFailedBadRequestException(string? message = null, int customCode = (int)CustomCodeEnum.DbUpdateFailed)
            : base(message, customCode) { }
    }
}
