using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPermissionService
    {
        Task<IEnumerable<Permission>> GetPermissions();
        Task<Permission?> GetPermissionById(int id);
        Task<Permission> InsertPermission(Permission permission);
        Task<Permission> DeletePermission(int id);
        Task<Permission> UpdatePermission(int id, Permission permission);
    }
}
