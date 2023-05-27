using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPermissionTypeRepository
    {
        Task<IEnumerable<PermissionType>> GetPermissionTypes();
        Task<PermissionType> GetPermissionTypeById(int id);
        void InsertPermissionType(PermissionType permissionType);
        void DeletePermissionType(PermissionType permissionType);
        void UpdatePermissionType(PermissionType currentPermissionType, PermissionType permissionTypeToUpdate);
    }
}
