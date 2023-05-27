using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace BusinessLogic
{
    public class PermissionService : IPermissionService
    {
        private IPermissionRepository permissionRepo;

        public PermissionService(IPermissionRepository permissionRepo)
        {
            this.permissionRepo = permissionRepo;
        }

        public async Task<IEnumerable<Permission>> GetPermissions()
        {
            return await permissionRepo.GetPermissions();
        }

        public async Task<Permission> GetPermissionById(int id)
        {
            return await permissionRepo.GetPermissionById(id);
        }

        public async Task InsertPermission(Permission permission)
        {
            validatePermission(permission);

            permissionRepo.InsertPermission(permission);
            await permissionRepo.Save();
        }

        public async Task DeletePermission(int id)
        {
            var permission = await permissionRepo.GetPermissionById(id);
            validateIfPermissionExists(permission);

            permissionRepo.DeletePermission(permission);
            await permissionRepo.Save();
        }

        public async Task UpdatePermission(int id, Permission permissionToUpdate)
        {
            var currentPermission = await permissionRepo.GetPermissionById(id);
            validateIfPermissionExists(currentPermission);
            validatePermission(permissionToUpdate);

            permissionRepo.UpdatePermission(currentPermission, permissionToUpdate);
            await permissionRepo.Save();
        }

        private void validatePermission(Permission permission)
        {
            if (permission == null || permission.EmployeeFirstName == string.Empty || permission.EmployeeLastName == string.Empty || permission.PermissionTypeId < 1)
                throw new BadRequestException();
        }

        private void validateIfPermissionExists(Permission permission)
        {
            if (permission == null)
                throw new BadRequestException();
        }
    }
}