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
            var certificateFingerprint = configuration["ElasticSettings:certificateFingerprint"];
            var user = configuration["ElasticSettings:user"];
            var password = configuration["ElasticSettings:password"];

            var settings = new ConnectionSettings(new Uri(baseUrl ?? "")).PrettyJson().CertificateFingerprint(certificateFingerprint).BasicAuthentication(user, password).DefaultIndex(index);
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
