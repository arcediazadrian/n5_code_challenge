using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPermissionTypeRepository
    {
        Task<IEnumerable<PermissionType>> GetPermissionTypes();
        Task<PermissionType> GetPermissionTypeById(int id);
        void InsertPermissionType(PermissionType permissionType);
        Task DeletePermissionType(int id);
        Task UpdatePermissionType(int id, PermissionType permissionType);
        Task Save();
    }
}
