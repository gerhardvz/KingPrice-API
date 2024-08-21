namespace MrPrice_Usermanager.Services;

public interface IGroupManagementService
{
    public Task AddGroup();

    public Task DeleteGroup();

    public Task UpdateGroup();

    public Task<int> Count();
    
    public Task<int> UserInGroupCount();
    public Task<int> PermissionsInGroupCount();
}