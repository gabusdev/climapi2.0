using Climapi.Core.Entities.Enums;
using Climapi.Services.Exceptions.BaseExceptions;

namespace Climapi.Services.Exceptions.NotFound
{
    public class UserNotFoundException : BaseNotFoundException
    {
        public UserNotFoundException(string? message = null, int customCode = (int)CustomCodeEnum.UserNotFound)
            : base(message, customCode) 
        {
            if (message == null)
            {
                CustomMessage = "There is not user with such Id";
            }
        }
        //public UserNotFoundException() : base() { }
    }
}