using KingPrice_Usermanager.Models;

namespace UsermanagerAPI_UT;

public static class TestDataHelper
{
    public static List<User> GetFakeEmployeeList()
    {
        return new List<User>
        {
            new()
            {
                Id = 1,
                Name = "John",
                Surname = "Doe",
                Email = "J.D@gmail.com"
            },
            new()
            {
                Id = 2,
                Name = "Mark Luther",
                Email = "M.L@gmail.com"
            }
        };
    }

    public static List<Group> GetFakeGroupsList()
    {
        return new List<Group>
        {
            new()
            {
                Id = 1,
                Name = "Admins",
                Users = [],
                Permissions = []
            },
            new()
            {
                Id = 2,
                Name = "Viewers",
                Users = [],
                Permissions = []
            },
            new()
            {
                Id = 3,
                Name = "Clients",
                Users = [],
                Permissions = []
            }
        };
    }
}