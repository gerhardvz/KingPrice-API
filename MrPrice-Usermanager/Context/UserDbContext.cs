using Microsoft.EntityFrameworkCore;
using MrPrice_Usermanager.Models;

namespace MrPrice_Usermanager.Context;

public class UserDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Group> Groups { set; get; }
    public DbSet<Permission> Permissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(b =>
        {
            b.HasMany<Group>(x => x.Groups)
                .WithMany(x => x.Users);
            b.HasKey(x => x.Id);
        });
        
        modelBuilder.Entity<Group>(b =>
        {
            b.HasMany<Permission>(x => x.Permissions)
                .WithMany(x => x.Groups);
            b.HasKey(x => x.Id);
        });
        
        modelBuilder.Entity<Permission>(b =>
        {
            b.HasKey(x => x.Id);
        });
    }
}