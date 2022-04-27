using AspNetCoreRateLimit;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Climapi.Api.AppServices.RateLimit
{
    public static class RateLimitExension
    {
        public static void ConfigureRateLimit(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddHttpContextAccessor();

            var rules = new List<RateLimitRule>
            {
                new RateLimitRule
                {
                    Endpoint = "*",
                    Limit = 1,
                    Period = "1s"
                }
            };
            services.Configure<IpRateLimitOptions>(o =>
            {
                o.GeneralRules = rules;
            });
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        }
    }
}
