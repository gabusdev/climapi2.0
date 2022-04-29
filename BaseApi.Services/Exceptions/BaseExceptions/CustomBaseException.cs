using System;

namespace Climapi.Services.Exceptions.BaseExceptions
{
    public class CustomBaseException : Exception
    {
        public int HttpCode { get; set; }
        public int CustomCode { get; set; }
        public string CustomMessage { get; set; } = null!;

        public CustomBaseException() : base()
        {
        }
    }
}