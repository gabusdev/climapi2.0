using FluentValidation;

namespace Climapi.Common.DTO.Request.Validations
{
    public class RegisterDTOValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDTOValidator()
        {
            RuleFor(r => r.Username).NotEmpty().MaximumLength(20);
            RuleFor(r => r.Email).EmailAddress().MaximumLength(20);
            RuleFor(r => r.Password).Length(6, 30);
            RuleFor(r => r.ConfirmationPassword).Equal(r => r.Password)
                .WithMessage("Password and Confirmation Password do not match.");
        }
    }
}
