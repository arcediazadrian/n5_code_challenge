using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetPermissions();
        Task<Permission?> GetPermissionById(int id);
        Task InsertPermission(Permission permission);
        Task DeletePermission(Permission permission);
        Task UpdatePermission(Permission currentPermission, Permission permissionToUpdate);
    }
}
