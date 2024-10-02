using Game_Zone.ViewModels.Game;

namespace Game_Zone.Controllers;
public class GamesController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IGameService _gameService;
    private readonly IDeviceService _deviceService;

    
    public GamesController(ICategoryService categoryService, IGameService gameService, IDeviceService deviceService)
    {
        _categoryService = categoryService;
        _gameService = gameService;
        _deviceService = deviceService;
    }

    public IActionResult Index()
    {

        return View(_gameService.GetAll());
    }
    public IActionResult Detail(int id)
    {
        var game = _gameService.GetById(id);
        if (game is null)
            return NotFound();
        return View(game);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var game = _gameService.GetById(id);

        if (game is null)
            return NotFound();
        
        EditGameVM vm = new ()
        {
            Id=game.Id,
            Name = game.Name,
            CategoryId = game.CategoryId,
            Description = game.Description,

            CurrentCover =game.Cover,

            SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),

            Categories = _categoryService.GetCategories(),
            Devices = _deviceService.GetDevices(),
        };
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditGameVM model)
    {

        if (!ModelState.IsValid)
        {
            CreateGameVM vm = new CreateGameVM()
            {
                Categories = _categoryService.GetCategories(),
                Devices = _deviceService.GetDevices(),
            };
            return View(vm);
        }

       var game= await _gameService.Edit(model);
        if (game is null)
            return BadRequest();

        return RedirectToAction(nameof(Index));
    }


    [HttpGet]
    public IActionResult Create()
    {
        CreateGameVM vm = new CreateGameVM()
        {
            Categories = _categoryService.GetCategories(),
            Devices = _deviceService.GetDevices(),
        };
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task <IActionResult> Create(CreateGameVM model)
    {
        CreateGameVM vm = new CreateGameVM()
        {
            Categories = _categoryService.GetCategories(),
            Devices = _deviceService.GetDevices(),
        };
        if(!ModelState.IsValid)
        return View(vm);

        await _gameService.Create(model);

        return RedirectToAction(nameof(Index));
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        bool IsDeleted = _gameService.Delete(id);

        return IsDeleted ? Ok() : BadRequest();
    }
}
