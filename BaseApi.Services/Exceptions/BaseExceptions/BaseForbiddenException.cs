using System.Net;

namespace Climapi.Services.Exceptions.BaseExceptions
{
    public class BaseForbiddenException : CustomBaseException
    {
        public BaseForbiddenException(string? message = null, int customCode = 0) : base()
        {
            HttpCode = (int)HttpStatusCode.Forbidden;
            CustomCode = 403000 + customCode;
            CustomMessage = message ?? CustomCode.ToString();
        }
    }
}