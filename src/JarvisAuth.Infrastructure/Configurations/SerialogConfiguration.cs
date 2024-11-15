using Elastic.Ingest.Elasticsearch;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace JarvisAuth.Infrastructure.Configurations
{
    public static class SerialogConfiguration
    {
        public static IHostBuilder ConfigureSerilog(this IHostBuilder hostBuilder, IConfiguration configuration)
        {
            var elasticSearchUrl = configuration["ElasticSearchUrl"];

            hostBuilder.UseSerilog((context, configuration) =>
            {
                configuration
                .WriteTo
                .Elasticsearch(new[] { new Uri(elasticSearchUrl) }, opts =>
                {
                    opts.DataStream = new DataStreamName("logs-api", "example", "demo");
                    opts.BootstrapMethod = BootstrapMethod.Failure;
                })
                .ReadFrom
                .Configuration(context.Configuration);
            });

            return hostBuilder;
        }
    }
}
