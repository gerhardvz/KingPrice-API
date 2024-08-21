namespace MrPrice_Usermanager.Services;

public interface IUserManagementService
{
    public Task AddUser();

    public Task DeleteUser();

    public Task UpdateUser();
    public Task<int> UserCount();
    
    
}