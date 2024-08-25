using KingPrice_Usermanager.Services;
using Microsoft.AspNetCore.Mvc;

namespace KingPrice_Usermanager.Controllers;

[ApiController]
[Route("[controller]")]
public class StatsController : ControllerBase
{
    private readonly IStatsService _statsService;

    public StatsController(IStatsService statsService)
    {
        _statsService = statsService;
    }

    /// <summary>
    ///     Get count of users in system
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Returns count</response>
    /// <response code="400">If problem occured</response>
    [HttpGet]
    [Route("users")]
    public async Task<IActionResult> GetUserCount()
    {
        try
        {
            var count = await _statsService.GetUserCount();
            return Ok(count);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Failed to get user count");
        }
    }

    /// <summary>
    ///     Get count of users in group
    /// </summary>
    /// <param name="groupId"></param>
    /// <returns></returns>
    /// <response code="200">Returns count</response>
    /// <response code="400">If problem occured</response>
    [HttpGet]
    [Route("group/{groupId}/users")]
    public async Task<IActionResult> GetUserCountInGroup([FromRoute] int groupId)
    {
        try
        {
            var count = await _statsService.GetUserCountInGroup(groupId);
            return Ok(count);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Failed to get user count in group");
        }
    }

    /// <summary>
    ///     Get count of groups in system
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Returns count</response>
    /// <response code="400">If problem occured</response>
    [HttpGet]
    [Route("group")]
    public async Task<IActionResult> GetGroupCount()
    {
        try
        {
            var count = await _statsService.GetGroupCount();
            return Ok(count);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Failed to get group count");
        }
    }

    /// <summary>
    ///     Get the count of permissions in a group
    /// </summary>
    /// <param name="groupId"></param>
    /// <returns></returns>
    /// <response code="200">Returns count</response>
    /// <response code="400">If problem occured</response>
    [HttpGet]
    [Route("group/{groupId}/permissions")]
    public async Task<IActionResult> GetPermissionCountInGroup([FromRoute] int groupId)
    {
        try
        {
            var count = await _statsService.GetPermissionCountInGroup(groupId);
            return Ok(count);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Failed to get permission count in group");
        }
    }

    /// <summary>
    ///     Get count of Permissions in system
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Returns count</response>
    /// <response code="400">If problem occured</response>
    [HttpGet]
    [Route("permissions")]
    public async Task<IActionResult> GetPermissionCount()
    {
        try
        {
            var count = await _statsService.GetPermissionCount();
            return Ok(count);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Failed to get permission count");
        }
    }
}