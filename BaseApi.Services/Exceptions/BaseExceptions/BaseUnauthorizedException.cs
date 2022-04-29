using System;
using System.Net;

namespace Climapi.Services.Exceptions.BaseExceptions
{
    public class BaseUnauthorizedException : CustomBaseException
    {
        public BaseUnauthorizedException(string? message = null, int customCode = 0) : base()
        {
            HttpCode = (int)HttpStatusCode.Unauthorized;
            CustomCode = 401000 + customCode;
            CustomMessage = message ?? CustomCode.ToString();
        }
    }
}