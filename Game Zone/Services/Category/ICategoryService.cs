namespace Game_Zone.Services.Category;

public interface ICategoryService
{
    IEnumerable<SelectListItem> GetCategories();
}
