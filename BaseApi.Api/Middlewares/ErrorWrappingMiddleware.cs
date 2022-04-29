using Climapi.Services.Exceptions.BaseExceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Climapi.Api.Middlewares
{
    public class ErrorWrappingMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorWrappingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        private string Message { get; set; } = null!;
        private int CustomStatusCode { get; set; }

        public async Task Invoke(HttpContext context)
        {
            CustomStatusCode = 500000;
            try
            {
                await _next.Invoke(context);
                return;
            }
            catch (CustomBaseException ex)
            {
                Message = ex.CustomMessage;
                CustomStatusCode = ex.CustomCode;
                context.Response.StatusCode = ex.HttpCode;
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                Message = ex.Message;
                var exMethod = context.Request.Method;
                var exPath = context.Request.Path;
                Log.Error(ex, $"Uncontrolled Error occurred at {exPath} with method {exMethod} and message: {Message}");
            }

            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";

                var response = new
                {
                    StausCode = context.Response.StatusCode,
                    Message = Message ?? context.Response.StatusCode.ToString(),
                    CustomStatusCode = CustomStatusCode
                };

                var json = JsonConvert.SerializeObject(response, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
                await context.Response.WriteAsync(json);
            }
        }
    }
}