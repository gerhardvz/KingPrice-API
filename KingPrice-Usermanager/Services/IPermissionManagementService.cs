using Microsoft.EntityFrameworkCore;
using MrPrice_Usermanager.Context;
using MrPrice_Usermanager.Models;

namespace MrPrice_Usermanager.Services;

public interface IPermissionManagementService
{
    /// <summary>
    /// Adds new Permission to DB
    /// </summary>
    /// <param name="permission"></param>
    /// <returns></returns>
    public Task AddPermission(Permission permission);

    /// <summary>
    /// Removes Permission form database
    /// </summary>
    /// <param name="id">Permission ID</param>
    /// <returns></returns>
    public Task DeletePermission(int id);

    /// <summary>
    /// Returns list of all Permissions in system
    /// </summary>
    /// <returns>Permission List</returns>
    public Task<List<Permission>> GetPermissions();
    /// <summary>
    /// Gets the Permission from ID
    /// </summary>
    /// <param name="id">Permission ID</param>
    /// <returns>Permission</returns>
    public Task<Permission?> GetPermission(int id);

    /// <summary>
    /// Returns Group/s where the Permission is added
    /// </summary>
    /// <param name="id">Permission ID</param>
    /// <returns>List of Groups where Permission is added</returns>
    public Task<List<Group>> GetPermissionGroups(int id);

}

public class PermissionManagementService:IPermissionManagementService
{
    private UserDbContext _db;

    public PermissionManagementService(UserDbContext db)
    {
        _db = db;
    }

    public async Task AddPermission(Permission permission)
    {
        _db.Permissions.Add(permission);
        await _db.SaveChangesAsync();
    }

    public async Task DeletePermission(int id)
    {
        var permission = await GetPermission(id);
        if (permission != null)
        {
            _db.Remove(permission);
        }
    }

    public Task<List<Permission>> GetPermissions()
    {
        return _db.Permissions.ToListAsync();
    }

    public Task<Permission?> GetPermission(int id)
    {
        var permission = _db.Permissions.FindAsync(id).AsTask();
        return permission;
    }

    public async Task<List<Group>> GetPermissionGroups(int id)
    {
        var permission = await GetPermission(id)??throw new Exception($"No Permission with ID {id}");
        return permission.Groups.ToList();
    }
}