using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPermissionService
    {
        Task<IEnumerable<Permission>> GetPermissions();
        Task<Permission> GetPermissionById(int id);
        Task InsertPermission(Permission permission);
        Task DeletePermission(int id);
        Task UpdatePermission(int id, Permission permission);
    }
}
