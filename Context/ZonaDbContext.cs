using Microsoft.EntityFrameworkCore;
using WebApiEsp32Master.Domain;

namespace WebApiEsp32Master.Context;

public class ZonaDbContext: DbContext
{
    public ZonaDbContext(DbContextOptions<ZonaDbContext> options): base(options)
    {
    }

    public DbSet<Zona> Zonas { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Tasks> Tasks { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Admin> Admins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=tcp:esp32server.database.windows.net,1433;Initial Catalog=Esp32Api;Persist Security Info=False;User ID=espmanager;Password=apiesp32#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Zona>()
            .Property(x => x.Datos)
            .HasConversion(
                v => string.Join(',' , v),
                v => v.Split(',' , StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()
            );
    }
}
