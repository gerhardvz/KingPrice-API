using KingPrice_Usermanager.Models;
using KingPrice_Usermanager.Services;
using Microsoft.AspNetCore.Mvc;

namespace KingPrice_Usermanager.Controllers;

[ApiController]
[Route("[controller]")]
public class PermissionsController : ControllerBase
{
    private readonly IPermissionManagementService _permissionManagementService;

    public PermissionsController(IPermissionManagementService permissionManagementService)
    {
        _permissionManagementService = permissionManagementService;
    }

    /// <summary>
    ///     Returns list of all groups in system
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var groups = await _permissionManagementService.GetPermissions();
            return Ok(groups);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Failed to Get Groups");
        }
    }

    /// <summary>
    ///     Returns Permission with ID
    /// </summary>
    /// <param name="id">group ID - long</param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        try
        {
            var group = await _permissionManagementService.GetPermission(id);
            if (group == null) return NoContent();

            return Ok(group);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest($"Failed to Get Group with ID: {id}");
        }
    }

    /// <summary>
    ///     Creates new group
    /// </summary>
    /// <param name="groupDto"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> AddPermission([FromBody] string groupName)
    {
        try
        {
            if (await _permissionManagementService.AddPermission(new Permission { Name = groupName }))
                return Ok("Permission added.");

            return BadRequest("Permission not added.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Failed to add Permission");
        }
    }

    /// <summary>
    ///     Removes group by their ID
    /// </summary>
    /// <param name="id">Permission ID - long</param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeletePermission([FromRoute] int id)
    {
        try
        {
            await _permissionManagementService.DeletePermission(id);
            return Ok("Group Deleted");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Failed to remove group");
        }
    }

    /// <summary>
    ///     Assigns user to group
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("{id}/AddGroup")]
    public async Task<IActionResult> AddGroupToPermission([FromRoute] int id, [FromQuery] int groupId)
    {
        try
        {
            await _permissionManagementService.AddGroupToPermission(groupId, id);
            return Ok("Group added to group");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Failed to add user to Permission");
        }
    }

    /// <summary>
    ///     Removes user from group
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}/RemoveGroup")]
    public async Task<IActionResult> RemoveGroupFromPermission([FromRoute] int id, [FromQuery] int userId)
    {
        try
        {
            await _permissionManagementService.RemoveGroupFromPermission(userId, id);
            return Ok("Group added to group");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Failed to add user to Permission");
        }
    }

    /// <summary>
    ///     Update group information
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdatePermission()
    {
        try
        {
            // _groupManagementService.UpdatePermission()
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}