namespace MrPrice_Usermanager.Models;

public class Group
{
    public int Id { get; set; }
    public string Type { get; set; }
    public List<User> Users { get; set; }
    public List<Permission> Permissions { get; set; }
}