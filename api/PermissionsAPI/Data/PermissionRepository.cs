using Domain.Entities;
using Domain.Interfaces;
using Nest;

namespace Data
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly PermissionsContext context;
        private readonly IElasticClient elasticClient;

        public PermissionRepository(PermissionsContext context, IElasticClient elasticClient)
        {
            this.context = context;
            this.elasticClient = elasticClient;
        }

        public async Task<IEnumerable<Permission>> GetPermissions()
        {
            var result = await elasticClient.SearchAsync<Permission>(s => s.Size(100));
            return result.Documents.ToList();
        }

        public async Task<Permission?> GetPermissionById(int id)
        {
            var result = await elasticClient.SearchAsync<Permission>(
                s => s.Size(1).Query(q => q.Match(m => m.Field(f => f.Id).Query(id.ToString()))));
            return result.Documents.ToList().First();
        }

        public async Task InsertPermission(Permission permission)
        {
            permission.GrantedDate = DateTime.Now;
            context.Permissions.Add(permission);
            await context.SaveChangesAsync();

            await elasticClient.IndexDocumentAsync(permission);
        }

        public async Task DeletePermission(Permission permission)
        {
            context.Permissions.Remove(permission);
            await context.SaveChangesAsync();

            await elasticClient.DeleteAsync<Permission>(permission);
        }

        public async Task UpdatePermission(Permission currentPermission, Permission permissionToUpdate)
        {
            currentPermission.EmployeeFirstName = permissionToUpdate.EmployeeFirstName;
            currentPermission.EmployeeLastName = permissionToUpdate.EmployeeLastName;
            currentPermission.PermissionTypeId = permissionToUpdate.PermissionTypeId;
            await context.SaveChangesAsync();

            await elasticClient.UpdateAsync<Permission>(currentPermission.Id, p => p.Index("permissions").Doc(currentPermission));
        }
    }
}
