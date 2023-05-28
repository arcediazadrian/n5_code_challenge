using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace BusinessLogic
{
    public class PermissionTypeService : IPermissionTypeService
    {
        private readonly IPermissionUnitOfWork permissionUnitOfWork;

        public PermissionTypeService(IPermissionUnitOfWork permissionUnitOfWork)
        {
            this.permissionUnitOfWork = permissionUnitOfWork;
        }

        public async Task<IEnumerable<PermissionType>> GetPermissionTypes()
        {
            return await permissionUnitOfWork.PermissionTypeRepository.GetPermissionTypes();
        }

        public async Task<PermissionType?> GetPermissionTypeById(int id)
        {
            return await permissionUnitOfWork.PermissionTypeRepository.GetPermissionTypeById(id);
        }

        public async Task<PermissionType> InsertPermissionType(PermissionType permissionType)
        {
            ValidateIfPermissionTypeIsValid(permissionType);

            permissionUnitOfWork.PermissionTypeRepository.InsertPermissionType(permissionType);
            await permissionUnitOfWork.Save();

            return permissionType;
        }

        public async Task<PermissionType> DeletePermissionType(int id)
        {
            var permissionType = await GetPermissionTypeById(id);
            ValidateIfPermissionTypeExists(permissionType);
            await ValidateIfPermissionTypeIsInUse(id);

            permissionUnitOfWork.PermissionTypeRepository.DeletePermissionType(permissionType);
            await permissionUnitOfWork.Save();

            return permissionType;
        }

        public async Task<PermissionType> UpdatePermissionType(int id, PermissionType permissionTypeToUpdate)
        {
            var permissionType = await GetPermissionTypeById(id);
            ValidateIfPermissionTypeExists(permissionType);

            permissionUnitOfWork.PermissionTypeRepository.UpdatePermissionType(permissionType, permissionTypeToUpdate);
            await permissionUnitOfWork.Save();

            return permissionType;
        }

        private void ValidateIfPermissionTypeIsValid(PermissionType permissionType)
        {
            if (permissionType == null || permissionType.Description == String.Empty)
                throw new ValidationException(message: "Permission Type is not valid.");
        }

        private void ValidateIfPermissionTypeExists(PermissionType permissionType)
        {
            if (permissionType == null)
                throw new ValidationException(message: "Permission Type does not exist.");
        }

        private async Task ValidateIfPermissionTypeIsInUse(int id)
        {
            var permissions = await permissionUnitOfWork.PermissionRepository.GetPermissions();
            if (permissions.Any(p => p.PermissionTypeId == id))
                throw new ValidationException(message: "Permission Type is in use by an existing Permission.");
        }
    }
}
