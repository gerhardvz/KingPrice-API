namespace MrPrice_Usermanager.Services;

public interface IPermissionManagementService
{
    public Task AddPermission();

    public Task DeletePermission();

    public Task UpdatePermission();
}