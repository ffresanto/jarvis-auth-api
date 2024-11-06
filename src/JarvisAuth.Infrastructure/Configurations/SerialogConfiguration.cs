using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Exceptions;

namespace JarvisAuth.Infrastructure.Configurations
{
    public class SerialogConfiguration
    {
        public static void ConfigureSerilog(IHostBuilder hostBuilder, IConfiguration configuration)
        {
            var logstashUrl = configuration["LogstashUrl"];

            hostBuilder.UseSerilog((context, config) =>
            {
                config
                    .ReadFrom.Configuration(configuration)
                    .Enrich.FromLogContext()
                    .Enrich.WithExceptionDetails()
                    .WriteTo.Console();

                if (!string.IsNullOrEmpty(logstashUrl))
                {
                    config.WriteTo.Http(logstashUrl, null);
                }
            });
        }
    }
}
