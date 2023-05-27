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

        public async Task<PermissionType> GetPermissionTypeById(int id)
        {
            return await permissionUnitOfWork.PermissionTypeRepository.GetPermissionTypeById(id);
        }

        public async Task InsertPermissionType(PermissionType permissionType)
        {
            ValidateIfPermissionTypeIsValid(permissionType);

            permissionUnitOfWork.PermissionTypeRepository.InsertPermissionType(permissionType);
            await permissionUnitOfWork.Save();
        }

        public async Task DeletePermissionType(int id)
        {
            var permissionType = await GetPermissionTypeById(id);
            ValidateIfPermissionTypeExists(permissionType);
            await ValidateIfPermissionTypeIsInUse(id);

            permissionUnitOfWork.PermissionTypeRepository.DeletePermissionType(permissionType);
            await permissionUnitOfWork.Save();
        }

        public async Task UpdatePermissionType(int id, PermissionType permissionTypeToUpdate)
        {
            var permissionType = await GetPermissionTypeById(id);
            ValidateIfPermissionTypeExists(permissionType);

            permissionUnitOfWork.PermissionTypeRepository.UpdatePermissionType(permissionType, permissionTypeToUpdate);
            await permissionUnitOfWork.Save();
        }

        private void ValidateIfPermissionTypeIsValid(PermissionType permissionType)
        {
            if (permissionType == null || permissionType.Description == String.Empty)
                throw new BadRequestException();
        }

        private void ValidateIfPermissionTypeExists(PermissionType permissionType)
        {
            if (permissionType == null)
                throw new BadRequestException();
        }

        private async Task ValidateIfPermissionTypeIsInUse(int id)
        {
            var permissions = await permissionUnitOfWork.PermissionRepository.GetPermissions();
            var permissionsUsingPermissionType = permissions.Where(p => p.PermissionTypeId == id);
            if (permissionsUsingPermissionType != null && permissionsUsingPermissionType.Any())
                throw new BadRequestException();
        }
    }
}
