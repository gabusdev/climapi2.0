using FluentValidation;

namespace Climapi.Common.DTO.Request.Validations
{
    public class LoginDTOValidator : AbstractValidator<LoginDto>
    {
        public LoginDTOValidator()
        {
            RuleFor(login => login.Email).EmailAddress().NotEmpty();
            RuleFor(login => login.Password).NotEmpty().Length(6, 30);
        }
    }
}
