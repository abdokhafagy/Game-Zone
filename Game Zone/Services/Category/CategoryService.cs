
namespace Game_Zone.Services.Category;

public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _context;

    public CategoryService(ApplicationDbContext context)
    {
        _context = context;
    }
    public IEnumerable<SelectListItem> GetCategories()
    {
        return _context.Categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).AsNoTracking().ToList();
    }
}
