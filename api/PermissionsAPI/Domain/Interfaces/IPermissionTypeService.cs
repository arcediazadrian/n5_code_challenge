using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPermissionTypeService
    {
        Task<IEnumerable<PermissionType>> GetPermissionTypes();
        Task<PermissionType?> GetPermissionTypeById(int id);
        Task<PermissionType> InsertPermissionType(PermissionType permissionType);
        Task<PermissionType> DeletePermissionType(int id);
        Task<PermissionType> UpdatePermissionType(int id, PermissionType permissionType);
    }
}
