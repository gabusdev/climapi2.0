using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace AppServices.MyLogging
{
    public static class LoggingExtension
    {
        public static Logger ConfigureLogger()
        {
            var logger = new LoggerConfiguration()
                .WriteTo.File(
                    path: "log\\log-.txt",
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day,
                    restrictedToMinimumLevel: LogEventLevel.Information
                    ).CreateLogger();
            return logger;
        }
    }
}
