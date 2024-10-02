
namespace Game_Zone.Services.Game;

public interface IGameService
{
    IEnumerable<Models.Game> GetAll();
    Models.Game? GetById(int id);
    Task Create(CreateGameVM model);
    Task<Models.Game?> Edit(EditGameVM model);
    bool Delete(int id);    
}
