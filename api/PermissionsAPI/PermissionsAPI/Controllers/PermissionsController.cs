using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PermissionsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private IPermissionService permissionService;

        public PermissionsController(IPermissionService permissionService)
        {
            this.permissionService = permissionService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var permissions = await permissionService.GetPermissions();

                if (permissions == null) { return NotFound(); }
                return Ok(permissions);
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
                var permission = await permissionService.GetPermissionById(id);

                if (permission == null) { return NotFound(); }
                return Ok(permission);
            }
            catch (BadRequestException badRequestException)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Permission permission)
        {
            try
            {
                await permissionService.InsertPermission(permission);
                return Ok();
            }
            catch (BadRequestException badRequestException)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Permission permissionToUpdate)
        {
            try
            {
                await permissionService.UpdatePermission(id, permissionToUpdate);
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
                await permissionService.DeletePermission(id);
                return Ok();
            }
            catch (BadRequestException badRequestException)
            {
                return BadRequest();
            }
        }
    }
}
