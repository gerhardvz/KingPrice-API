using KingPrice_Usermanager.Context;
using Microsoft.EntityFrameworkCore;

namespace KingPrice_Usermanager.Services;

public interface IStatsService
{
    /// <summary>
    ///     Returns the amount of Users registered on the system
    /// </summary>
    /// <param name="token">Cancellation Token</param>
    /// <returns>Count</returns>
    public Task<int> GetUserCount(CancellationToken token = default);

    /// <summary>
    ///     Returns the amount of Users registered on the system under a specific Group
    /// </summary>
    /// <param name="groupId">Group ID</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns></returns>
    public Task<int> GetUserCountInGroup(int groupId, CancellationToken token = default);

    /// <summary>
    ///     Returns the amount of Groups registered on the system
    /// </summary>
    /// <param name="token">Cancellation Token</param>
    /// <returns></returns>
    public Task<int> GetGroupCount(CancellationToken token = default);

    /// <summary>
    ///     Returns the amount of Permissions registered on the system under a specific Group
    /// </summary>
    /// <param name="groupId"></param>
    /// <param name="token">Cancellation Token</param>
    /// <returns></returns>
    public Task<int> GetPermissionCountInGroup(int groupId, CancellationToken token = default);

    /// <summary>
    ///     Returns the amount of Permissions registered on the system
    /// </summary>
    /// <param name="token">Cancellation Token</param>
    /// <returns></returns>
    public Task<int> GetPermissionCount(CancellationToken token = default);
}

public class StatsService : IStatsService
{
    private readonly UserDbContext _db;
    private ILogger<StatsService> _logger;

    public StatsService(UserDbContext db, ILogger<StatsService> logger)
    {
        _db = db;
        _logger = logger;
    }

    /// <inheritdoc />
    public Task<int> GetUserCount(CancellationToken token = default)
    {
        return _db.Users.CountAsync(token);
    }

    /// <inheritdoc />
    public async Task<int> GetUserCountInGroup(int groupId, CancellationToken token = default)
    {
        var group = await _db.Groups.FindAsync(groupId) ?? throw new Exception("Group not found");
        if (group.Users == null) return 0;

        await _db.Entry(group).Collection(x => x.Users).LoadAsync(token);

        return group.Users.Count;
    }

    /// <inheritdoc />
    public Task<int> GetGroupCount(CancellationToken token = default)
    {
        return _db.Groups.CountAsync(token);
    }

    /// <inheritdoc />
    public async Task<int> GetPermissionCountInGroup(int groupId, CancellationToken token = default)
    {
        var group = await _db.Groups.FindAsync(groupId) ?? throw new Exception("Group not found");
        if (group.Permissions == null) return 0;
        await _db.Entry(group).Collection(x => x.Permissions).LoadAsync(token);
        return group.Permissions.Count;
    }

    /// <inheritdoc />
    public Task<int> GetPermissionCount(CancellationToken token = default)
    {
        return _db.Permissions.CountAsync(token);
    }
}