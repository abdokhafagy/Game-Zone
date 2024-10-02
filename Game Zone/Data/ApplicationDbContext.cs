
namespace Game_Zone.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Game> Games { get; set; }
    public DbSet<GameDevice> GameDevices { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<GameDevice>()
            .HasKey(id => new { id.GameId, id.DeviceId });

        modelBuilder.Entity<Category>()
            .HasData(new Category[]
            {
                new Category{ Id = 1, Name = "Sports" } ,
                new Category{ Id = 2, Name = "Action" } ,
                new Category{ Id = 3, Name = "Film" } ,
                new Category{ Id = 4, Name = "Racing" } ,
                new Category{ Id = 5, Name = "Fighting" } ,

            });

        modelBuilder.Entity<Device>()
           .HasData(new Device[]
           {
                new Device{ Id = 1, Name = "Playstation" , Icon="bi bi-playstation"} ,
                new Device{ Id = 2, Name = "xbox" , Icon="bi bi-xbox"} ,
                new Device{ Id = 3, Name = "PC" , Icon="bi bi-pc-display" } ,
                
           });

    }

}

