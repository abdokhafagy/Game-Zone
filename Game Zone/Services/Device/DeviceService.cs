
namespace Game_Zone.Services.Device;

public class DeviceService : IDeviceService
{
    private readonly ApplicationDbContext _context;

    public DeviceService(ApplicationDbContext context)
    {
        _context = context;
    }
    public IEnumerable<SelectListItem> GetDevices() =>
   _context.Devices.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).AsNoTracking().ToList();
    
}
