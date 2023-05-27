using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace BusinessLogic
{
    public class PermissionTypeService : IPermissionTypeService
    {
        private IPermissionTypeRepository permissionTypeRepo;

        public PermissionTypeService(IPermissionTypeRepository permissionTypeRepo)
        {
            this.permissionTypeRepo = permissionTypeRepo;
        }

        public async Task<IEnumerable<PermissionType>> GetPermissionTypes()
        {
            return await permissionTypeRepo.GetPermissionTypes();
        }

        public async Task<PermissionType> GetPermissionTypeById(int id)
        {
            return await permissionTypeRepo.GetPermissionTypeById(id);
        }

        public async Task InsertPermissionType(PermissionType permissionType)
        {
            validateIfPermissionTypeIsValid(permissionType);

            permissionTypeRepo.InsertPermissionType(permissionType);
            await permissionTypeRepo.Save();
        }

        public async Task DeletePermissionType(int id)
        {
            var permissionType = await GetPermissionTypeById(id);
            validateIfPermissionTypeExists(permissionType);

            permissionTypeRepo.DeletePermissionType(permissionType);
            await permissionTypeRepo.Save();
        }

        public async Task UpdatePermissionType(int id, PermissionType permissionTypeToUpdate)
        {
            var permissionType = await GetPermissionTypeById(id);
            validateIfPermissionTypeExists(permissionType);

            permissionTypeRepo.UpdatePermissionType(permissionType, permissionTypeToUpdate);
            await permissionTypeRepo.Save();
        }

        private void validateIfPermissionTypeIsValid(PermissionType permissionType)
        {
            if (permissionType == null || permissionType.Description == String.Empty)
                throw new BadRequestException();
        }

        private void validateIfPermissionTypeExists(PermissionType permissionType)
        {
            if (permissionType == null)
                throw new BadRequestException();
        }
    }
}
