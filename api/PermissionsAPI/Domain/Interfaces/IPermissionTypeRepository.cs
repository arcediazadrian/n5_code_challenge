using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPermissionTypeRepository
    {
        Task<IEnumerable<PermissionType>> GetPermissionTypes();
        Task<PermissionType?> GetPermissionTypeById(int id);
        Task InsertPermissionType(PermissionType permissionType);
        Task DeletePermissionType(PermissionType permissionType);
        Task UpdatePermissionType(PermissionType currentPermissionType, PermissionType permissionTypeToUpdate);
    }
}
