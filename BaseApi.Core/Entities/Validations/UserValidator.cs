using Climapi.Core.Entities;
using FluentValidation;

namespace Climapi.Core.Validations
{
    public class UserValidator : AbstractValidator<AppUser>
    {
        public UserValidator()
        {

        }
    }
}
