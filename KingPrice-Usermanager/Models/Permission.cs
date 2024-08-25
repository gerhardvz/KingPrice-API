namespace KingPrice_Usermanager.Models;

public class Permission
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual List<Group> Groups { get; set; } = new();
}