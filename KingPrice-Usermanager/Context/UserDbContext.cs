using Microsoft.EntityFrameworkCore;
using MrPrice_Usermanager.Models;

namespace MrPrice_Usermanager.Context;

public class UserDbContext : DbContext
{
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Group> Groups { set; get; }
    public virtual DbSet<Permission> Permissions { get; set; }

   

    /// <inheritdoc />
    public UserDbContext(DbContextOptions<UserDbContext> options):base(options)
    {
      
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(b =>
        {
            b.HasMany<Group>(x => x.Groups)
                .WithMany(x => x.Users);
            b.HasKey(x => x.Id);
            b.HasIndex(x => x.Email).IsUnique();
            
        });
        
        modelBuilder.Entity<Group>(b =>
        {
            b.HasMany<Permission>(x => x.Permissions)
                .WithMany(x => x.Groups);
            b.HasKey(x => x.Id);
            b.HasIndex(x => x.Name);
        });
        
        modelBuilder.Entity<Permission>(b =>
        {
            b.HasKey(x => x.Id);
            b.HasIndex(x => x.Name);
        });
    }
}