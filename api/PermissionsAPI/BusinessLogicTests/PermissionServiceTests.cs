using BusinessLogic;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Moq;

namespace BusinessLogicTests
{
    public class PermissionServiceTests
    {
        private readonly PermissionService service;
        private readonly Mock<IPermissionUnitOfWork> unitOfWork;
        public PermissionServiceTests()
        {
            unitOfWork = new Mock<IPermissionUnitOfWork>();

            service = new PermissionService(unitOfWork.Object);
        }

        [Fact]
        public async void InsertPermission_ShouldSucceed()
        {
            //arrange
            Permission permission = new Permission
            {
                EmployeeFirstName = "Adrian",
                EmployeeLastName = "Arce",
                PermissionTypeId = 1,
            };
            unitOfWork.Setup(u => u.PermissionRepository.InsertPermission(permission));
            unitOfWork.Setup(u => u.PermissionTypeRepository.GetPermissionTypeById(permission.PermissionTypeId)).Returns(Task.FromResult<PermissionType>(new PermissionType { Id = 1, Description = "Administrator" }));

            //act
            await service.InsertPermission(permission);

            //assert
            unitOfWork.Verify(u => u.Save(), Times.Once);
        }

        [Fact]
        public async void InsertPermission_ShouldFail_WhenFirstNameIsEmpty()
        {
            //arrange
            Permission permission = new Permission
            {
                EmployeeFirstName = string.Empty,
                EmployeeLastName = "Arce",
                PermissionTypeId = 1,
            };
            unitOfWork.Setup(u => u.PermissionRepository.InsertPermission(permission));

            //act
            Task act() => service.InsertPermission(permission);

            //assert
            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void InsertPermission_ShouldFail_WhenLastNameIsEmpty()
        {
            //arrange
            Permission permission = new Permission
            {
                EmployeeFirstName = "Adrian",
                EmployeeLastName = string.Empty,
                PermissionTypeId = 1,
            };
            unitOfWork.Setup(u => u.PermissionRepository.InsertPermission(permission));

            //act
            Task act() => service.InsertPermission(permission);

            //assert
            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void InsertPermission_ShouldFail_WhenPermissionTypeIdIsLessThanOrEqualToZero()
        {
            //arrange
            Permission permission = new Permission
            {
                EmployeeFirstName = "Adrian",
                EmployeeLastName = "Arce",
                PermissionTypeId = 0,
            };
            unitOfWork.Setup(u => u.PermissionRepository.InsertPermission(permission));

            //act
            Task act() => service.InsertPermission(permission);

            //assert
            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void InsertPermission_ShouldFail_WhenPermissionTypeDoesNotExist()
        {
            //arrange
            Permission permission = new Permission
            {
                EmployeeFirstName = "Adrian",
                EmployeeLastName = "Arce",
                PermissionTypeId = 999,
            };
            unitOfWork.Setup(u => u.PermissionRepository.InsertPermission(permission));
            unitOfWork.Setup(u => u.PermissionTypeRepository.GetPermissionTypeById(permission.PermissionTypeId)).Returns(Task.FromResult<PermissionType>(null));

            //act
            Task act() => service.InsertPermission(permission);

            //assert
            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void UpdatePermission_ShouldFail_WhenPermissionDoesNotExist()
        {
            //arrange
            int id = 1;
            Permission permission = new Permission
            {
                EmployeeFirstName = "Adrian",
                EmployeeLastName = "Arce",
                PermissionTypeId = 999,
            };
            unitOfWork.Setup(u => u.PermissionRepository.GetPermissionById(id)).Returns(Task.FromResult<Permission>(null));

            //act
            Task act() => service.UpdatePermission(id, permission);

            //assert
            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async void DeletePermission_ShouldFail_WhenPermissionDoesNotExist()
        {
            //arrange
            int id = 1;
            unitOfWork.Setup(u => u.PermissionRepository.GetPermissionById(id)).Returns(Task.FromResult<Permission>(null));

            //act
            Task act() => service.DeletePermission(id);

            //assert
            await Assert.ThrowsAsync<BadRequestException>(act);
        }
    }
}