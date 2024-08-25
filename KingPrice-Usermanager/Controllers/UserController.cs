using Microsoft.AspNetCore.Mvc;
using MrPrice_Usermanager.Models;
using MrPrice_Usermanager.Services;


namespace MrPrice_Usermanager.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserManagementService _userManagementService;

    public UserController(IUserManagementService userManagementService)
    {
        _userManagementService = userManagementService;
    }

    /// <summary>
    /// Returns list of all users in system
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var users = _userManagementService.GetUsers();
            return Ok(users);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest($"Failed to Get Users");
        }
    }

    /// <summary>
    /// Returns User with ID
    /// </summary>
    /// <param name="id">user ID - long</param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get([FromRoute] long id)
    {
        try
        {
            var user = _userManagementService.GetUser(id);
            if (user == null)
            {
                return NoContent();
            }

            return Ok(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest($"Failed to Get User with ID: {id}");
        }
    }

    /// <summary>
    /// Creates new user
    /// </summary>
    /// <param name="userDto"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> AddUser([FromBody] UserCreateDTO userDto)
    {
        try
        {
            if ( _userManagementService.AddUser(userDto.Name, userDto.Surname, userDto.Email))
            {
                return Ok("User added.");
            }

            return BadRequest("User not added.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Failed to add user");
        }

       
    }

    /// <summary>
    /// Removes user by their ID
    /// </summary>
    /// <param name="id">User ID - long</param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] long id)
    {
        try
        {
            _userManagementService.DeleteUser(id);
            return Ok("User Deleted");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Failed to remove user");
        }
    }

    /// <summary>
    /// Update user information
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateUser()
    {
        try
        {
            // _userManagementService.UpdateUser()
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
       
    }
}