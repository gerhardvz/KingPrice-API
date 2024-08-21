namespace MrPrice_Usermanager.Models;

public class User
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public List<Group> Groups { get; set; }
}