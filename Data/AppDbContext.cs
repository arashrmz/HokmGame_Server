using Microsoft.EntityFrameworkCore;

namespace HokmGame_Server.Data;
public class AppDbContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public AppDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var serverVersion = MySqlServerVersion.AutoDetect(Configuration.GetConnectionString("WebApiDatabase"));

        // connect to sql server with connection string from app settings
        options.UseMySql(Configuration.GetConnectionString("WebApiDatabase"), serverVersion);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Friend>()
                .HasOne(f => f.User)
                .WithMany(p => p.FriendsOf)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Friend>()
            .HasOne(f => f.FriendUser)
            .WithMany(p => p.FriendsTo)
            .HasForeignKey(f => f.FriendId);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Friend> Friendships { get; set; }
}