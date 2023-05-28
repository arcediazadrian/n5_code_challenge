using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace BusinessLogic
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionUnitOfWork permissionUnitOfWork;

        public PermissionService(IPermissionUnitOfWork permissionUnitOfWork)
        {
            this.permissionUnitOfWork = permissionUnitOfWork;
        }

        public async Task<IEnumerable<Permission>> GetPermissions()
        {
            return await permissionUnitOfWork.PermissionRepository.GetPermissions();
        }

        public async Task<Permission?> GetPermissionById(int id)
        {
            return await permissionUnitOfWork.PermissionRepository.GetPermissionById(id);
        }

        public async Task<Permission> InsertPermission(Permission permission)
        {
            ValidatePermission(permission);
            await ValidateIfPermissionTypeExists(permission.PermissionTypeId);

            permissionUnitOfWork.PermissionRepository.InsertPermission(permission);
            await permissionUnitOfWork.Save();

            return permission;
        }

        public async Task<Permission> DeletePermission(int id)
        {
            var permission = await permissionUnitOfWork.PermissionRepository.GetPermissionById(id);
            ValidateIfPermissionExists(permission);

            permissionUnitOfWork.PermissionRepository.DeletePermission(permission);
            await permissionUnitOfWork.Save();

            return permission;
        }

        public async Task<Permission> UpdatePermission(int id, Permission permissionToUpdate)
        {
            var currentPermission = await permissionUnitOfWork.PermissionRepository.GetPermissionById(id);
            ValidateIfPermissionExists(currentPermission);
            ValidatePermission(permissionToUpdate);
            await ValidateIfPermissionTypeExists(permissionToUpdate.PermissionTypeId);

            permissionUnitOfWork.PermissionRepository.UpdatePermission(currentPermission, permissionToUpdate);
            await permissionUnitOfWork.Save();

            return currentPermission;
        }

        private void ValidatePermission(Permission permission)
        {
            if (permission == null || permission.EmployeeFirstName == string.Empty || permission.EmployeeLastName == string.Empty || permission.PermissionTypeId < 1)
                throw new ValidationException(message: "Permission is not valid.");
        }

        private void ValidateIfPermissionExists(Permission permission)
        {
            if (permission == null)
                throw new ValidationException(message: "Permission does not exist.");
        }

        private async Task ValidateIfPermissionTypeExists(int id)
        {
            var permissionType = await permissionUnitOfWork.PermissionTypeRepository.GetPermissionTypeById(id);
            if (permissionType == null)
                throw new ValidationException(message: "Permission Type does not exists.");
        }
    }
}