namespace Game_Zone.ViewModels.Game;

public class CreateGameVM : GameVM
{

    [AllowedExtensions(FileSetting.AllowedExtensions, FileSetting.MaxFilesSizesInBytes)]
    public IFormFile Cover { get; set; } = default!;

}
