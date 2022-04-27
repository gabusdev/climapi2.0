using System;
using System.Net;

namespace Climapi.Services.Exceptions.BaseExceptions
{
    public class BaseForbiddenException : CustomBaseException
    {
        public BaseForbiddenException(string? message = null, int customCode = 0) : base()
        {
            HttpCode = (int)HttpStatusCode.Forbidden;
            CustomCode = 403000 + (Convert.ToBoolean(customCode) ? customCode : 0);
            CustomMessage = message ?? CustomCode.ToString();
        }
    }
}