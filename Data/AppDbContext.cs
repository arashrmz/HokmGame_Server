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

    public DbSet<User> Users { get; set; }
}