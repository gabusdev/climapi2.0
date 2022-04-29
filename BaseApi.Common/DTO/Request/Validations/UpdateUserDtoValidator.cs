using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Climapi.Common.DTO.Request.Validations
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(r => r.Username).NotEmpty().MaximumLength(20);
            RuleFor(r => r.Email).EmailAddress().MaximumLength(20);
        }
    }
}
