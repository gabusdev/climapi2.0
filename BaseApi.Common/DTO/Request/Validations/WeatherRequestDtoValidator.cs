using FluentValidation;

namespace Climapi.Common.DTO.Request.Validations
{
    internal class WeatherRequestDtoValidator : AbstractValidator<WeatherRequestDto>
    {
        public WeatherRequestDtoValidator()
        {
            //RuleFor(request => request.ApiKey).NotEmpty().Length(10).WithMessage("Must provide Api Key");
            RuleFor(request => request.Query).NotEmpty().WithMessage("Must include parameter");
        }
    }
}
