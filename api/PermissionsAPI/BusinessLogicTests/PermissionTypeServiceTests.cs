using BusinessLogic;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Moq;

namespace BusinessLogicTests
{
    public class PermissionTypeServiceTests
    {
        private readonly PermissionTypeService service;
        private readonly Mock<IPermissionUnitOfWork> unitOfWork;
        public PermissionTypeServiceTests()
        {
            unitOfWork = new Mock<IPermissionUnitOfWork>();

            service = new PermissionTypeService(unitOfWork.Object);
        }

        [Fact]
        public async void InsertPermissionType_ShouldSucceed()
        {
            //arrange
            PermissionType permissionType = new PermissionType
            {
                Description = "Administrator",
            };
            unitOfWork.Setup(u => u.PermissionTypeRepository.InsertPermissionType(permissionType));

            //act
            await service.InsertPermissionType(permissionType);

            //assert
            unitOfWork.Verify(u => u.Save(), Times.Once);
        }

        [Fact]
        public async void InsertPermissionType_ShouldFail_WhenDescriptionIsEmpty()
        {
            //arrange
            PermissionType permissionType = new PermissionType
            {
                Description = String.Empty,
            };
            unitOfWork.Setup(u => u.PermissionTypeRepository.InsertPermissionType(permissionType));

            //act
            Task act() => service.InsertPermissionType(permissionType);

            //assert
            await Assert.ThrowsAsync<ValidationException>(act);
        }

        [Fact]
        public async void UpdatePermissionType_ShouldFail_WhenPermissionTypeDoesNotExist()
        {
            //arrange
            int id = 1;
            PermissionType permissionType = new PermissionType
            {
                Description = String.Empty,
            };
            unitOfWork.Setup(u => u.PermissionTypeRepository.GetPermissionTypeById(id)).Returns(Task.FromResult<PermissionType>(null));

            //act
            Task act() => service.UpdatePermissionType(id, permissionType);

            //assert
            await Assert.ThrowsAsync<ValidationException>(act);
        }

        [Fact]
        public async void DeletePermissionType_ShouldFail_WhenPermissionTypeDoesNotExist()
        {
            //arrange
            int id = 1;
            unitOfWork.Setup(u => u.PermissionTypeRepository.GetPermissionTypeById(id)).Returns(Task.FromResult<PermissionType>(null));

            //act
            Task act() => service.DeletePermissionType(id);

            //assert
            await Assert.ThrowsAsync<ValidationException>(act);
        }

        [Fact]
        public async void DeletePermissionType_ShouldFail_WhenPermissionIsInUse()
        {
            //arrange
            int id = 1;
            PermissionType permissionType = new PermissionType
            {
                Id = id,
                Description = "Administrator",
            };

            List<Permission> permissions = new List<Permission>
            {
                new Permission
                {
                    PermissionTypeId = id,
                },
                new Permission
                {
                    PermissionTypeId = 123,
                },
                new Permission
                {
                    PermissionTypeId = id,
                },
            };


            unitOfWork.Setup(u => u.PermissionTypeRepository.GetPermissionTypeById(id)).Returns(Task.FromResult<PermissionType>(permissionType));
            unitOfWork.Setup(u => u.PermissionRepository.GetPermissions()).Returns(Task.FromResult<IEnumerable<Permission>>(permissions));

            //act
            Task act() => service.DeletePermissionType(id);

            //assert
            await Assert.ThrowsAsync<ValidationException>(act);
        }
    }
}