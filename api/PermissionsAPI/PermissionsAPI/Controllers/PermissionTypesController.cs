using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security;

namespace PermissionsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionTypesController : ControllerBase
    {
        private readonly IPermissionTypeService permissionTypeService;

        public PermissionTypesController(IPermissionTypeService permissionTypeService)
        {
            this.permissionTypeService = permissionTypeService;
        }

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PermissionType>>> Get()
        {
            try
            {
                var permissionTypes = await permissionTypeService.GetPermissionTypes();
                return Ok(permissionTypes);
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
        public async Task<ActionResult<PermissionType>> GetById(int id)
        {
            try
            {
                var permissionType = await permissionTypeService.GetPermissionTypeById(id);
                if (permissionType == null) return NotFound();
                return Ok(permissionType);
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
        public async Task<ActionResult<PermissionType>> Post([FromBody] PermissionType permissionType)
        {
            try
            {
                var createdPermissionType = await permissionTypeService.InsertPermissionType(permissionType);
                return CreatedAtAction(nameof(GetById), new { id = createdPermissionType.Id }, createdPermissionType);
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
        public async Task<ActionResult<PermissionType>> Put(int id, [FromBody] PermissionType permissionTypeToUpdate)
        {
            try
            {
                await permissionTypeService.UpdatePermissionType(id, permissionTypeToUpdate);
                return Ok();
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
        public async Task<ActionResult<PermissionType>> Delete(int id)
        {
            try
            {
                await permissionTypeService.DeletePermissionType(id);
                return Ok();
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
