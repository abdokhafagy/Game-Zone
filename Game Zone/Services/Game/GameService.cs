
namespace Game_Zone.Services.Game;
public class GameService : IGameService
{
    private readonly ApplicationDbContext _context;
    private readonly string _imagepath;
    private readonly IWebHostEnvironment _host;

    public GameService(ApplicationDbContext context, IWebHostEnvironment host)
    {
        _context = context;
        _host = host;
        _imagepath =$"{_host.WebRootPath}{FileSetting.ImagePath}";
    }
    public IEnumerable<Models.Game> GetAll()
    {
        return _context.Games
            .Include(c=>c.Category)
            .Include(d=>d.Devices)
            .ThenInclude(d=>d.Device)
            .AsNoTracking().ToList();
    }
    public Models.Game? GetById(int id)
    {
        return _context.Games
            .Include(c => c.Category)
            .Include(d => d.Devices)
            .ThenInclude(d => d.Device)
            .AsNoTracking()
            .SingleOrDefault(g=>g.Id==id);
    }
    public async Task Create(CreateGameVM model)
    {
        string covername = await SaveCover(model.Cover);

        Models.Game game = new()
        {
            Name=model.Name,
            CategoryId=model.CategoryId,
            Description=model.Description,
            Cover=covername,
            Devices=model.SelectedDevices.Select(d=>new GameDevice(){DeviceId=d }).ToList()
        };

        await _context.AddAsync(game);
        await _context.SaveChangesAsync();

    }

    public async Task<Models.Game?> Edit(EditGameVM model)
    {
        var game = await _context.Games
            .Include(g => g.Devices)
            .SingleOrDefaultAsync(s => s.Id == model.Id);

        if (game is null)
            return null;

        var hascover = model.Cover is not null;
        var oldcover = game.Cover;


        game.Name = model.Name;
        game.Description = model.Description;
        game.CategoryId = model.CategoryId;
        game.Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();

        if (hascover)
        {
            game.Cover = await SaveCover(model.Cover!);
        }
        var effectrow = _context.SaveChanges();
        if (effectrow > 0)
        {
            if (hascover)
            {
                var cover = Path.Combine(_imagepath, oldcover);
                File.Delete(cover);
            }
            return game;
        }
        else
        {
            var cover = Path.Combine(_imagepath, game.Cover);
            File.Delete(cover);
            return null;
        }

    }


    private async Task<string> SaveCover(IFormFile cover)
    {
        string covername = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";

        var path = Path.Combine(_imagepath, covername);

        using var stream = File.Create(path);
        await cover.CopyToAsync(stream);
        return covername;
    }

    public bool Delete(int id)
    {
        var game = _context.Games.Find(id);
        if (game == null)
            return false;

        _context.Remove(game);
        var effectrow = _context.SaveChanges();
        if (effectrow > 0)
        {
            var cover = Path.Combine(_imagepath, game.Cover);
            File.Delete(cover);
            return true;
        }
        else
            return false;
    }
}
