using System.Net;

namespace Climapi.Services.Exceptions.BaseExceptions
{
    public class BaseNotFoundException : CustomBaseException
    {
        public BaseNotFoundException(string? message = null, int customCode = 0) : base()
        {
            HttpCode = (int)HttpStatusCode.NotFound;
            CustomCode = 404000 + customCode;
            CustomMessage = message ?? CustomCode.ToString();
        }
    }
}