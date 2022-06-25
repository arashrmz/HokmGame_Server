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
        modelBuilder.Entity<Friendship>()
       .HasKey(f => new { f.SenderId, f.FriendId });

        modelBuilder.Entity<Friendship>()
            .HasOne(f => f.Sender)
            .WithMany(u => u.Friends)
            .HasForeignKey(f => f.SenderId);

        modelBuilder.Entity<Friendship>()
            .HasOne(f => f.Friend)
            .WithMany(u => u.Friends)
            .HasForeignKey(f => f.FriendId);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Friendship> Friendships { get; set; }
}