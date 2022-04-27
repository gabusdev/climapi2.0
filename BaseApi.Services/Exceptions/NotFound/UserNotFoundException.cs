using Climapi.Services.Exceptions.BaseExceptions;

namespace Climapi.Services.Exceptions.NotFound
{
    public class UserNotFoundException : BaseNotFoundException
    {
        public UserNotFoundException(string message, int customCode)
            : base(message, customCode) { }
        public UserNotFoundException() : base() { }
    }
}