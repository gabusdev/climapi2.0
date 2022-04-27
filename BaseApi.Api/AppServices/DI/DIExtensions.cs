﻿using Climapi.Services;
using Climapi.Services.Impl;
using DataEF.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Climapi.Api.AppServices.DI
{
    public static class DIExtensions
    {
        public static void ConfigureDI(this IServiceCollection services)
        {
            services.AddScoped<IAuthManagerService, AuthManagerService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
