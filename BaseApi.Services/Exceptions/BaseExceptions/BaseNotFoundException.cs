using System;
using System.Net;

namespace Climapi.Services.Exceptions.BaseExceptions
{
    public class BaseNotFoundException : CustomBaseException
    {
        public BaseNotFoundException(string? message = null, int customCode = 0) : base()
        {
            HttpCode = (int)HttpStatusCode.NotFound;
            CustomCode = 404000 + (Convert.ToBoolean(customCode) ? customCode : 0);
            CustomMessage = message ?? CustomCode.ToString();
        }
    }
}