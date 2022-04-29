using AspNetCoreRateLimit;

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
                    Limit = 2,
                    Period = "1s"
                },
                new RateLimitRule
                {
                    Endpoint = "*",
                    Limit = 30,
                    Period = "1m"
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
