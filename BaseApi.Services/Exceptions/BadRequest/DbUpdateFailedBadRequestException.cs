using Climapi.Core.Entities.Enums;
using Climapi.Services.Exceptions.BaseExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Climapi.Services.Exceptions.BadRequest
{
    public class DbUpdateFailedBadRequestException: BaseBadRequestException
    {
        public DbUpdateFailedBadRequestException(string? message = null, int customCode = (int)CustomCodeEnum.DbUpdateFailed)
            : base(message, customCode) { }
    }
}
