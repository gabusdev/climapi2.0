﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Climapi.Api.AppServices.Swagger
{
    public class ConfigureSwaggerUIOptions : IConfigureNamedOptions<SwaggerUIOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerUIOptions(
            IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }
        public void Configure(string name, SwaggerUIOptions options)
        {
            Configure(options);
        }

        public void Configure(SwaggerUIOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        }
    }
}
