using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Climapi.Api.AppServices.Jwt
{
    public static class JwtExtension
    {
        public static void ConfigureJwt(this IServiceCollection services, IConfiguration config, bool encrypt = false)
        {
            var jwtSettings = config.GetSection("Jwt");
            // Procure add key and secret in Enviroment for security
            var key = Environment.GetEnvironmentVariable("JwtKey")
                ?? jwtSettings.GetValue<string>("Key");

            var tokenParams = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
                ValidAudience = jwtSettings.GetValue<string>("Audience"),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            };
            if (encrypt)
            {
                var secret = Environment.GetEnvironmentVariable("JwtSecret")
                    ?? jwtSettings.GetValue<string>("Secret");
                tokenParams.TokenDecryptionKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            }

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = tokenParams;
            });
        }
    }
}
