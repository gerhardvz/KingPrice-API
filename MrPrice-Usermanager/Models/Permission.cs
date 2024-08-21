namespace MrPrice_Usermanager.Models;

public class Permission
{
    public int Id { get; set; }
    public string Type { get; set; }
    public List<Group> Groups { get; set; }
}