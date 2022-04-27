using Climapi.Core.Validations;
using Common.DTO.Request.Validations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

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
