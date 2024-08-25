using KingPrice_Usermanager.Models;
using Microsoft.EntityFrameworkCore;

namespace KingPrice_Usermanager.Context;

public class UserDbContext : DbContext
{
    /// <inheritdoc />
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    protected UserDbContext()
    {
    }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Group> Groups { set; get; }
    public virtual DbSet<Permission> Permissions { get; set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();
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