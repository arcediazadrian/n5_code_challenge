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
            unitOfWork.Setup(u => u.PermissionTypeRepository.GetPermissionTypeById(permission.PermissionTypeId)).Returns(Task.FromResult(new PermissionType { Id = 1, Description = "Administrator" }));

            //act
            var createdPermission = await service.InsertPermission(permission);

            //assert
            Assert.NotNull(createdPermission);
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
            await Assert.ThrowsAsync<ValidationException>(act);
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
            await Assert.ThrowsAsync<ValidationException>(act);
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
            await Assert.ThrowsAsync<ValidationException>(act);
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
            await Assert.ThrowsAsync<ValidationException>(act);
        }

        [Fact]
        public async void UpdatePermission_ShouldSucceed()
        {
            //arrange
            int id = 1;
            Permission currentPermission = new Permission
            {
                Id = id,
                EmployeeFirstName = "Adrian",
                EmployeeLastName = "Arce",
                PermissionTypeId = 1,
            };
            Permission permissionToUpdate = new Permission
            {
                Id = id,
                EmployeeFirstName = "Adrian",
                EmployeeLastName = "Arze",
                PermissionTypeId = 2,
            };

            unitOfWork.Setup(u => u.PermissionRepository.GetPermissionById(id)).Returns(Task.FromResult<Permission>(currentPermission));
            unitOfWork.Setup(u => u.PermissionTypeRepository.GetPermissionTypeById(It.IsAny<int>())).Returns(Task.FromResult(new PermissionType() { Description = "Manager" }));
            unitOfWork.Setup(u => u.PermissionRepository.UpdatePermission(currentPermission, permissionToUpdate)).Returns(Task.FromResult<Permission>(permissionToUpdate));
            

            //act
            var updatedPermission = await service.UpdatePermission(id, permissionToUpdate);

            //assert
            Assert.NotNull(updatedPermission);
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
            await Assert.ThrowsAsync<ValidationException>(act);
        }

        [Fact]
        public async void UpdatePermission_ShouldFail_WhenPermissionToUpdateIsInvalid()
        {
            //arrange
            int id = 1;
            Permission permission = new Permission
            {
                EmployeeFirstName = String.Empty,
                EmployeeLastName = String.Empty,
                PermissionTypeId = 1,
            };
            unitOfWork.Setup(u => u.PermissionRepository.GetPermissionById(id)).Returns(Task.FromResult<Permission>(null));

            //act
            Task act() => service.UpdatePermission(id, permission);

            //assert
            await Assert.ThrowsAsync<ValidationException>(act);
        }

        [Fact]
        public async void UpdatePermission_ShouldFail_WhenPermissionTypeDoesNotExist()
        {
            //arrange
            int id = 1;
            Permission currentPermission = new Permission
            {
                Id = id,
                EmployeeFirstName = "Adrian",
                EmployeeLastName = "Arce",
                PermissionTypeId = 1,
            };
            Permission permissionToUpdate = new Permission
            {
                Id = id,
                EmployeeFirstName = "Adrian",
                EmployeeLastName = "Arze",
                PermissionTypeId = 999,
            };

            unitOfWork.Setup(u => u.PermissionRepository.GetPermissionById(id)).Returns(Task.FromResult<Permission>(currentPermission));
            unitOfWork.Setup(u => u.PermissionTypeRepository.GetPermissionTypeById(permissionToUpdate.PermissionTypeId)).Returns(Task.FromResult<PermissionType>(null));


            //act
            Task act() => service.UpdatePermission(id, permissionToUpdate);

            //assert
            await Assert.ThrowsAsync<ValidationException>(act);
        }

        [Fact]
        public async void DeletePermission_ShouldSucceed()
        {
            //arrange
            int id = 1;
            Permission permission = new Permission
            {
                Id = id,
                EmployeeFirstName = "Adrian",
                EmployeeLastName = "Arce",
                PermissionTypeId = 1,
            };
            unitOfWork.Setup(u => u.PermissionRepository.GetPermissionById(id)).Returns(Task.FromResult<Permission>(permission));

            //act
            var deletedPermission = await service.DeletePermission(id);

            //assert
            Assert.NotNull(deletedPermission);
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
            await Assert.ThrowsAsync<ValidationException>(act);
        }
    }
}