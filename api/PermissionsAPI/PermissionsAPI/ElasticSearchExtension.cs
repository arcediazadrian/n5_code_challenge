using Domain.Entities;
using Nest;

namespace PermissionsAPI
{
    public static class ElasticSearchExtension
    {
        public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            var baseUrl = configuration["ElasticSettings:baseUrl"];
            var index = configuration["ElasticSettings:defaultIndex"];
            var settings = new ConnectionSettings(new Uri(baseUrl ?? "")).PrettyJson().CertificateFingerprint("50110905cf2b6ba757971b346e866ae64b6e78364e2a6c003bd2f307f344ca16").BasicAuthentication("elastic", "Hc0qcFsfCWT=x8U_1iLa").DefaultIndex(index);
            settings.EnableApiVersioningHeader();
            AddDefaultMappings(settings);
            var client = new ElasticClient(settings);
            services.AddSingleton<IElasticClient>(client);
            CreateIndex(client, index);
        }
        private static void AddDefaultMappings(ConnectionSettings settings)
        {
            settings.DefaultMappingFor<Permission>(m => m);
            settings.DefaultMappingFor<PermissionType>(m => m);
        }
        private static void CreateIndex(IElasticClient client, string indexName)
        {
            var createIndexResponse = client.Indices.Create(indexName, index => index.Map<Permission>(x => x.AutoMap()));
        }
    }
}
