namespace KingPrice_Usermanager.Models;

public class User
{
    public User()
    {
    }

    public User(string name, string surname, string email)
    {
        Name = name;
        Surname = surname;
        Email = email;
    }

    public User(string name, string surname, string email, List<Group> groups)
    {
        Name = name;
        Surname = surname;
        Email = email;
        Groups = groups;
    }

    public User(long id, string name, string surname, string email, List<Group> groups)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Email = email;
        Groups = groups;
    }


    public long Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public virtual List<Group> Groups { get; set; } = new();

    public override string ToString()
    {
        return $"{Id} - {Name} {Surname} ({Email})";
    }
}