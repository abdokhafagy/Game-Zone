namespace Game_Zone.Services.Device;

public interface IDeviceService
{
    IEnumerable<SelectListItem> GetDevices();
}
