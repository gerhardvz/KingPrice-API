using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MrPrice_Usermanager.Models;
using MrPrice_Usermanager.Services;

namespace MrPrice_Usermanager.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupController : ControllerBase
{
    private IGroupManagementService _groupManagementService;

    public GroupController(IGroupManagementService groupManagementService)
    {
        _groupManagementService = groupManagementService;
    }

    /// <summary>
    /// Returns list of all groups in system
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var groups = await _groupManagementService.GetGroups();
            return Ok(groups);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest($"Failed to Get Users");
        }
    }

    /// <summary>
    /// Returns Group with ID
    /// </summary>
    /// <param name="id">group ID - long</param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        try
        {
            var group = await _groupManagementService.GetGroup(id);
            if (group == null)
            {
                return NoContent();
            }
            
            return Ok(group);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest($"Failed to Get User with ID: {id}");
        }
    }

    /// <summary>
    /// Creates new group
    /// </summary>
    /// <param name="groupDto"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> AddGroup([FromBody] string groupName)
    {
        try
        {
            if (await _groupManagementService.AddGroup(new Group() { Name = groupName }))
            {
                return Ok("Group added.");
            }

            return BadRequest("Group not added.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Failed to add Group");
        }
    }

    /// <summary>
    /// Removes group by their ID
    /// </summary>
    /// <param name="id">Group ID - long</param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteGroup([FromRoute] int id)
    {
        try
        {
            await _groupManagementService.DeleteGroup(id);
            return Ok("Group Deleted");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Failed to remove group");
        }
    }

    /// <summary>
    /// Assigns user to group
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("{id}/AddUser")]
    public async Task<IActionResult> AddUserToGroup([FromRoute] int id,[FromQuery] long userId)
    {
        try
        {
            await _groupManagementService.AddUserToGroup(userId, id);
            return Ok("User added to group");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Failed to add user to Group");
        }
    }
    
    ///<summary>
    /// Removes user from group
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}/RemoveUser")]
    public async Task<IActionResult> RemoveUserFromGroup([FromRoute] int id,[FromQuery] long userId)
    {
        try
        {
            await _groupManagementService.RemoveUserFromGroup(userId, id);
            return Ok("User added to group");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Failed to add user to Group");
        }
    }

    /// <summary>
    /// Update group information
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateGroup()
    {
        try
        {
            // _groupManagementService.UpdateGroup()
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}