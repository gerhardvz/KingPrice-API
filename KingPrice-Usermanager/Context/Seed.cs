using KingPrice_Usermanager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KingPrice_Usermanager.Context;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Group>();
        SeedGroups(modelBuilder.Entity<Group>());
        SeedPermissions(modelBuilder.Entity<Permission>());
    }

    private static void SeedGroups(EntityTypeBuilder<Group> modelBuilder)
    {//Add Groups to DB on Creation
        modelBuilder.HasData(
            new Group
            {
                Id = 1,
                Name = "Admin",
               
            },
            new Group
            {
                Id = 2,
                Name = "Viewer"
            });
    } 
    private static void SeedPermissions(EntityTypeBuilder<Permission> modelBuilder)
    {
        //Add Permissions to DB on creation
        modelBuilder.HasData(
            new Permission()
            {
                Id = 1,
                Name = "Read"
            },
            new Permission
            {
                Id = 2,
                Name = "Write"
            },
            new Permission
            {
                Id = 3,
                Name = "Create"
            },
            new Permission
            {
                Id = 4,
                Name = "Delete"
            });
    }

    
}