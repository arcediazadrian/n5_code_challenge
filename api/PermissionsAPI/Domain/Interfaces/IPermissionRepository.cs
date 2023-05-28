using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetPermissions();
        Task<Permission?> GetPermissionById(int id);
        void InsertPermission(Permission permission);
        void DeletePermission(Permission permission);
        void UpdatePermission(Permission currentPermission, Permission permissionToUpdate);
    }
}
