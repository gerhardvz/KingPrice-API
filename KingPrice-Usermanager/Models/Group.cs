namespace MrPrice_Usermanager.Models;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual List<User> Users { get; set; }=new ();
    public virtual List<Permission> Permissions { get; set; }=new ();
}