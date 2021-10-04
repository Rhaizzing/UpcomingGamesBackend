using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using UpcomingGames.Database;
using UpcomingGames.Database.Entities;

namespace UpcomingGames.API.Repositories
{
	public class GameRepository
	{
		private readonly postgresContext _dbContext;

		public GameRepository(postgresContext dbContext)
		{
			_dbContext = dbContext;
		}

		public ValueTask<GameEntity?> GetById(int id)
		{
			return _dbContext.Games.FindAsync(id);
		}

		public Task<List<GameEntity>> GetAll(int page, int pageSize)
		{
			return _dbContext.Games.Skip((page - 1) * pageSize)
				.Take(pageSize).ToListAsync();
		}

		public Task<List<GameEntity>> SearchByName(string name)
		{
			return _dbContext.Games.Where(game => game.Name.ToLower().Contains(name.ToLower())).ToListAsync();
		}

		public ValueTask<EntityEntry<GameEntity>> Add(GameEntity game)
		{
			return _dbContext.Games.AddAsync(game);
		}

		public int SaveChanges()
			=> _dbContext.SaveChanges();
	}
}