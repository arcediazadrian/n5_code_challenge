using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace PermissionsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService permissionService;

        public PermissionsController(IPermissionService permissionService)
        {
            this.permissionService = permissionService;
        }

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Permission>>> Get()
        {
            try
            {
                var permissions = await permissionService.GetPermissions();
                return Ok(permissions);
            }
            catch (ValidationException validationException)
            {
                return BadRequest(validationException.Message);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpGet("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Permission>> GetById(int id)
        {
            try
            {
                var permission = await permissionService.GetPermissionById(id);

                if (permission == null) { return NotFound(); }
                return Ok(permission);
            }
            catch (ValidationException validationException)
            {
                return BadRequest(validationException.Message);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Permission>> Post([FromBody] Permission permission)
        {
            try
            {
                var createdPermission = await permissionService.InsertPermission(permission);
                return CreatedAtAction(nameof(GetById), new { id = createdPermission.Id }, createdPermission);
            }
            catch (ValidationException validationException)
            {
                return BadRequest(validationException.Message);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Permission>> Put(int id, [FromBody] Permission permissionToUpdate)
        {
            try
            {
                var permission = await permissionService.UpdatePermission(id, permissionToUpdate);
                return Ok(permission);
            }
            catch (ValidationException validationException)
            {
                return BadRequest(validationException.Message);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpDelete("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Permission>> Delete(int id)
        {
            try
            {
                var permission = await permissionService.DeletePermission(id);
                return Ok(permission);
            }
            catch (ValidationException validationException)
            {
                return BadRequest(validationException.Message);
            }
            catch
            {
                return Problem();
            }
        }
    }
}
