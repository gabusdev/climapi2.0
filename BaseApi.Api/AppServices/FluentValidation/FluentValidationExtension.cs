using Climapi.Common.DTO.Request.Validations;
using Climapi.Core.Validations;
using FluentValidation;

namespace Climapi.Api.AppServices.FluentValidation
{
    public static class FluentValidationExtension
    {
        public static void ConfigureFluentValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<UserValidator>();
            services.AddValidatorsFromAssemblyContaining<LoginDTOValidator>();
        }
    }
}
