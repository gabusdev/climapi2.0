using FluentValidation;

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
