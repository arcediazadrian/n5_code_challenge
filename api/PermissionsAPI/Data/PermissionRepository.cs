using Domain.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class PermissionRepository: IPermissionRepository
    {
        private readonly PermissionsContext context;

        public PermissionRepository(PermissionsContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Permission>> GetPermissions()
        {
            await context.Permissions.Include(p => p.PermissionType).LoadAsync();

            return context.Permissions.OrderBy(b => b.Id).ToList();
        }

        public async Task<Permission?> GetPermissionById(int id)
        {
            var permission = await context.Permissions.Include(p => p.PermissionType).FirstOrDefaultAsync(p => p.Id == id);
            return permission;
        }

        public void InsertPermission(Permission permission)
        {
            permission.GrantedDate = DateTime.Now;
            context.Permissions.Add(permission);
        }

        public void DeletePermission(Permission permission)
        {
            context.Permissions.Remove(permission);
        }

        public void UpdatePermission(Permission currentPermission, Permission permissionToUpdate)
        {
            currentPermission.EmployeeFirstName = permissionToUpdate.EmployeeFirstName;
            currentPermission.EmployeeLastName = permissionToUpdate.EmployeeLastName;
            currentPermission.PermissionTypeId = permissionToUpdate.PermissionTypeId;
        }
    }
}
