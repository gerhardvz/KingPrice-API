using KingPrice_Usermanager.Context;
using KingPrice_Usermanager.Models;
using Microsoft.EntityFrameworkCore;

namespace KingPrice_Usermanager.Services;

public interface IPermissionManagementService
{
    /// <summary>
    ///     Adds new Permission to DB
    /// </summary>
    /// <param name="permission"></param>
    /// <returns></returns>
    public Task<bool> AddPermission(Permission permission);

    public Task<bool> AddGroupToPermission(int groupId, int permissionId);
    public Task<bool> RemoveGroupFromPermission(int groupId, int permissionId);

    /// <summary>
    ///     Removes Permission form database
    /// </summary>
    /// <param name="id">Permission ID</param>
    /// <returns></returns>
    public Task DeletePermission(int id);

    /// <summary>
    ///     Returns list of all Permissions in system
    /// </summary>
    /// <returns>Permission List</returns>
    public Task<List<Permission>> GetPermissions();

    /// <summary>
    ///     Gets the Permission from ID
    /// </summary>
    /// <param name="id">Permission ID</param>
    /// <returns>Permission</returns>
    public Task<Permission?> GetPermission(int id);

    /// <summary>
    ///     Returns Group/s where the Permission is added
    /// </summary>
    /// <param name="id">Permission ID</param>
    /// <returns>List of Groups where Permission is added</returns>
    public Task<List<Group>> GetPermissionGroups(int id);
}

public class PermissionManagementService : IPermissionManagementService
{
    private readonly UserDbContext _db;

    public PermissionManagementService(UserDbContext db)
    {
        _db = db;
    }

    public async Task<bool> AddPermission(Permission permission)
    {
        if (_db.Permissions.Any(x => x.Name.ToLower() == permission.Name.ToLower())) return false;

        _db.Permissions.Add(permission);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AddGroupToPermission(int groupId, int permissionId)
    {
        var group = await _db.Groups.FindAsync(groupId);
        var permission = await GetPermission(permissionId);
        //If both user and group is not null
        if (group != null && permission != null)
        {
            permission.Groups.Add(group);
            await _db.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<bool> RemoveGroupFromPermission(int groupId, int permissionId)
    {
        var group = await _db.Groups.FindAsync(groupId);
        var permission = await GetPermission(permissionId);
        //If both user and group is not null
        if (group != null && permission != null)
        {
            permission.Groups.Remove(group);
            await _db.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task DeletePermission(int id)
    {
        var permission = await GetPermission(id);
        if (permission != null) _db.Remove(permission);
    }

    public Task<List<Permission>> GetPermissions()
    {
        return _db.Permissions.ToListAsync();
    }

    public async Task<Permission?> GetPermission(int id)
    {
        var permission = await _db.Permissions.FindAsync(id).AsTask();
        await _db.Entry(permission).Collection(x => x.Groups).LoadAsync();
        return permission;
    }

    public async Task<List<Group>> GetPermissionGroups(int id)
    {
        var permission = await GetPermission(id) ?? throw new Exception($"No Permission with ID {id}");
        return permission.Groups.ToList();
    }
}