using Microsoft.EntityFrameworkCore;
using MrPrice_Usermanager.Context;
using MrPrice_Usermanager.Models;

namespace MrPrice_Usermanager.Services;

public interface IUserManagementService
{
    public bool AddUser(string name, string surname, string email);
    public bool AddUser(User user);

    public User? GetUser(long id);
    public List<User> GetUsers();
    public bool DeleteUser(long id);

    public bool UpdateUser(User user);
    public int UserCount();
}

public class UserManagementService : IUserManagementService
{
    private UserDbContext _db { get; set; }


    public UserManagementService(UserDbContext db)
    {
        _db = db;
    }

    public bool AddUser(string name, string surname, string email)
    {
        var nUser = new User(name, surname, email);
        //Search if email exists in the system

        if (!_db.Users.Any(x => x.Email == email))
        {
            //If not add user to db

            _db.Users.Add(nUser);
            _db.SaveChanges();

            return true;
        }

        return false;
    }


    public bool AddUser(User user)
    {
        if (!_db.Users.Any(x => x.Email == user.Email))
        {
            //If not add user to db

            _db.Users.Add(user);
            _db.SaveChanges();

            return true;
        }

        return false;
    }

    public User? GetUser(long id)
    {
        var user = _db.Users.Find(id);
        _db.Entry(user).Collection(g => g.Groups).Load();
        return user;
    }

    public List<User> GetUsers()
    {
        return _db.Users.ToList();
    }

    public bool DeleteUser(long id)
    {
        var user = GetUser(id);
        if (user == null)
        {
            //user does not exist so can't delete
            return false;
        }

        try
        {
            _db.Remove(user);
            _db.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.Error.WriteLineAsync($"Failed to remove {user}.\n {e.Message}");
            return false;
        }
    }

    public bool UpdateUser(User user)
    {
        throw new NotImplementedException();
    }

    public int UserCount()
    {
        return _db.Users.Count();
    }
}