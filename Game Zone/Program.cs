

namespace Game_Zone;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var conn = builder.Configuration.GetConnectionString("DefaultConnection") 
            ?? throw new InvalidOperationException("No Connection string");
        // Add services to the container.
        builder.Services.AddControllersWithViews();
        // Add Db Context .
        builder.Services.AddDbContext<ApplicationDbContext>(
            op=>op.UseSqlServer(conn)
            );

        // Services
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IDeviceService, DeviceService>();
        builder.Services.AddScoped<IGameService, GameService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
