using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PermissionsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionTypesController : ControllerBase
    {
        private IPermissionTypeService permissionTypeService;

        public PermissionTypesController(IPermissionTypeService permissionTypeService)
        {
            this.permissionTypeService = permissionTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var permissionTypes = await permissionTypeService.GetPermissionTypes();
                return Ok(permissionTypes);
            }
            catch (BadRequestException badRequestException)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var permissionType = await permissionTypeService.GetPermissionTypeById(id);
                return Ok(permissionType);
            }
            catch (BadRequestException badRequestException)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PermissionType permissionType)
        {
            try
            {
                await permissionTypeService.InsertPermissionType(permissionType);
                return Ok();
            }
            catch (BadRequestException badRequestException)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PermissionType permissionTypeToUpdate)
        {
            try
            {
                await permissionTypeService.UpdatePermissionType(id, permissionTypeToUpdate);
                return Ok();
            }
            catch (BadRequestException badRequestException)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await permissionTypeService.DeletePermissionType(id);
                return Ok();
            }
            catch (BadRequestException badRequestException)
            {
                return BadRequest();
            }
        }
    }
}
