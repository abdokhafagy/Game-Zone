using Game_Zone.Helper;

namespace Game_Zone.ViewModels.Game;

public class EditGameVM : GameVM
{
    public int Id { get; set; }
    public string? CurrentCover { get; set; }

    [AllowedExtensions(FileSetting.AllowedExtensions, FileSetting.MaxFilesSizesInBytes)]
    public IFormFile? Cover { get; set; } = default!;

}
