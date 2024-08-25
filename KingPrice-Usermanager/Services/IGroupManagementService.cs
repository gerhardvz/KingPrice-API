using KingPrice_Usermanager.Context;
using KingPrice_Usermanager.Models;
using Microsoft.EntityFrameworkCore;

namespace KingPrice_Usermanager.Services;

public interface IGroupManagementService
{
    public Task<Group?> GetGroup(int Id);
    public Task<List<Group>> GetGroups();

    public Task<bool> AddGroup(Group group);

    public Task DeleteGroup(int id);

    public Task UpdateGroup(Group group);

    public Task<int> Count();
    public Task RemoveUserFromGroup(long userId, int groupId);
    public Task AddUserToGroup(long userId, int groupId);
    public Task AddUserToGroup(long userId, Group group);
    public Task AddUserToGroup(User user, Group group);
    public Task<int> UserInGroupCount(Group group);
    public Task<int> PermissionsInGroupCount(Group group);
}

public class GroupManagementService : IGroupManagementService
{
    private readonly UserDbContext _db;
    private ILogger<GroupManagementService> _logger;

    public GroupManagementService(UserDbContext db, ILogger<GroupManagementService> logger)
    {
        _db = db;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<Group?> GetGroup(int Id)
    {
        var group = await _db.Groups.FindAsync(Id).AsTask();
        await _db.Entry(group).Collection(g => g.Users).LoadAsync();
        await _db.Entry(group).Collection(g => g.Permissions).LoadAsync();
        return group;
    }

    /// <inheritdoc />
    public Task<List<Group>> GetGroups()
    {
        var group = _db.Groups.ToListAsync();
        return group;
    }

    /// <inheritdoc />
    public async Task<bool> AddGroup(Group group)
    {
        if (_db.Groups.Any(x => x.Name.ToLower() == group.Name.ToLower())) return false;

        await _db.AddAsync(group).AsTask();
        await _db.SaveChangesAsync();
        return true;
    }

    /// <inheritdoc />
    public async Task DeleteGroup(int id)
    {
        var group = await GetGroup(id);
        if (group != null)
            _db.Groups.Remove(group);
    }

    /// <inheritdoc />
    public async Task UpdateGroup(Group group)
    {
        var g = await GetGroup(group);
        if (g != null)
        {
        }
    }

    /// <inheritdoc />
    public Task<int> Count()
    {
        return _db.Groups.CountAsync();
    }

    public async Task RemoveUserFromGroup(long userId, int groupId)
    {
        var user = await _db.Users.FindAsync(userId);
        var g = await GetGroup(groupId);
        //If both user and group is not null
        if (user != null && g != null)
        {
            g.Users.Remove(user);
            await _db.SaveChangesAsync();
        }
    }


    /// <inheritdoc />
    public async Task AddUserToGroup(long userId, int groupId)
    {
        var user = await _db.Users.FindAsync(userId);
        var g = await GetGroup(groupId);
        //If both user and group is not null
        if (user != null && g != null)
        {
            g.Users.Add(user);
            await _db.SaveChangesAsync();
        }
    }

    /// <inheritdoc />
    public async Task AddUserToGroup(long userId, Group group)
    {
        var user = await _db.Users.FindAsync(userId);
        var g = await GetGroup(group);
        //If both user and group is not null
        if (user != null && g != null)
        {
            g.Users.Add(user);
            await _db.SaveChangesAsync();
        }
    }

    /// <inheritdoc />
    public Task AddUserToGroup(User user, Group group)
    {
        //Use existing Method to AddUser to grop
        //Simplifies code and makes it easier to maintain same functionality
        return AddUserToGroup(user.Id, group);
    }

    /// <inheritdoc />
    public async Task<int> UserInGroupCount(Group group)
    {
        var g = await GetGroup(group) ?? throw new Exception($"Group not Found {group}");
        return g.Users.Count;
    }

    public async Task<int> PermissionsInGroupCount(Group group)
    {
        var g = await GetGroup(group) ?? throw new Exception($"Group not Found {group}");
        return g.Permissions.Count;
    }

    public async Task<Group?> GetGroup(Group group)
    {
        var g = await _db.Groups.FindAsync(group.Id).AsTask();
        if (g == null) g = await _db.Groups.FirstOrDefaultAsync(x => x.Name == group.Name);
        return g;
    }
}