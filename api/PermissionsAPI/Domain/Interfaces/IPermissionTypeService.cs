using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPermissionTypeService
    {
        Task<IEnumerable<PermissionType>> GetPermissionTypes();
        Task<PermissionType> GetPermissionTypeById(int id);
        Task InsertPermissionType(PermissionType permissionType);
        Task DeletePermissionType(int id);
        Task UpdatePermissionType(int id, PermissionType permissionType);
    }
}
