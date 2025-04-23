using Microsoft.EntityFrameworkCore;
using ZooProjectApi.Models;

namespace ZooProjectApi;

public class AnimalDbContext : DbContext
{
    public DbSet<Animal> Animals { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=AnimalBase;Username=postgres;Password=mypassword");
    }
}
