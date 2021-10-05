using Microsoft.EntityFrameworkCore.ChangeTracking;
using UpcomingGamesBackend.Model.Entities;

namespace UpcomingGamesBackend.Model.Interfaces;

public interface IGameRepository
{
    ValueTask<GameEntity?> GetById(int id);
    Task<int> GetAllItemsCount();
    Task<List<GameEntity>> GetAll(int page, int pageSize);
    Task<List<GameEntity>> SearchByName(string name);
    ValueTask<EntityEntry<GameEntity>> Add(GameEntity game);
    int SaveChanges();
}