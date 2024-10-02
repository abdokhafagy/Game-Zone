namespace Game_Zone.Helper;

public static class FileSetting
{
    public const string ImagePath = "/assets/uploaded images/games";
    public const string AllowedExtensions = ".jpg,.png,.jpeg";
    public const int MaxFilesSizesInMb = 1;
    public const int MaxFilesSizesInBytes = MaxFilesSizesInMb * 1024 * 1024;

}
