using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Climapi.Common.DTO.Request.Validations
{
    public class ResetPasswordDtoValidator: AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordDtoValidator()
        {
            RuleFor(r => r.Email).EmailAddress().NotEmpty();
            RuleFor(r => r.ResetToken).NotEmpty();
            RuleFor(r => r.NewPassword).NotEmpty().Length(6, 30);
            RuleFor(r => r.ConfirmationPassword).Equal(r => r.NewPassword)
                .WithMessage("New Password and Confirmation Password do not match.");
        }
    }
}
