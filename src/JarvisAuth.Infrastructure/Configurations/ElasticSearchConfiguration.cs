using Elastic.Clients.Elasticsearch;
using Elastic.Ingest.Elasticsearch;
using Elastic.Serilog.Sinks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace JarvisAuth.Infrastructure.Configurations
{
    public static class ElasticSearchConfiguration
    {
        public static IHostBuilder ConfigureElasticSearch(this IHostBuilder hostBuilder, IConfiguration configuration, string environment)
        {
            var elasticSearchUrl = configuration["ElasticSearchUrl"];

            if (PingElasticSearch(elasticSearchUrl).Result)
            {
                hostBuilder.UseSerilog((context, configuration) =>
                {
                    configuration
                    .WriteTo
                    .Elasticsearch(new[] { new Uri(elasticSearchUrl) }, opts =>
                    {
                        opts.DataStream = new Elastic.Ingest.Elasticsearch.DataStreams.DataStreamName("logs-api", "jarvis-auth", $"{environment}");
                        opts.BootstrapMethod = BootstrapMethod.Failure;
                    })
                    .ReadFrom
                    .Configuration(context.Configuration);
                });
                ;
            }

            return hostBuilder;
        }

        private static async Task<bool> PingElasticSearch(string elasticsearchUri)
        {
            try
            {
                var client = new ElasticsearchClient(new ElasticsearchClientSettings(new Uri(elasticsearchUri)));
                var pingResponse = await client.PingAsync();
                return pingResponse.IsValidResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error pinging ElasticSearch: {ex.Message}");
                return false;
            }
        }
    }
}
