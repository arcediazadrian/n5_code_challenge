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
            await validateIfPermissionTypeExists(id);

            await permissionTypeRepo.DeletePermissionType(id);
            await permissionTypeRepo.Save();
        }

        public async Task UpdatePermissionType(int id, PermissionType permissionTypeToUpdate)
        {
            validateIfPermissionTypeIsValid(permissionTypeToUpdate);
            await validateIfPermissionTypeExists(id);

            await permissionTypeRepo.UpdatePermissionType(id, permissionTypeToUpdate);
            await permissionTypeRepo.Save();
        }

        private void validateIfPermissionTypeIsValid(PermissionType permissionType)
        {
            if (permissionType == null || permissionType.Description == String.Empty)
                throw new BadRequestException();
        }

        private async Task validateIfPermissionTypeExists(int id)
        {
            var permissionType = await permissionTypeRepo.GetPermissionTypeById(id);
            if (permissionType == null)
                throw new BadRequestException();
        }
    }
}
