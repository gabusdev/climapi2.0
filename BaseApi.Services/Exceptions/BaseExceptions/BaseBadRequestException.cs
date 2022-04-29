using System.Net;

namespace Climapi.Services.Exceptions.BaseExceptions
{
    public class BaseBadRequestException : CustomBaseException
    {
        public BaseBadRequestException(string? message = null, int customCode = 0) : base()
        {
            HttpCode = (int)HttpStatusCode.BadRequest;
            CustomCode = 400000 + customCode;
            CustomMessage = message ?? CustomCode.ToString();
        }
    }
}