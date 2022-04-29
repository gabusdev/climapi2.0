using FluentValidation;

namespace Climapi.Common.DTO.Request.Validations
{
    public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordDtoValidator()
        {
            RuleFor(r => r.Password).NotEmpty().Length(6, 30);
            RuleFor(r => r.NewPassword).NotEmpty().Length(6, 30);
            RuleFor(r => r.ConfirmationPassword).Equal(r => r.NewPassword)
                .WithMessage("New Password and Confirmation Password do not match.");
        }
    }
}
