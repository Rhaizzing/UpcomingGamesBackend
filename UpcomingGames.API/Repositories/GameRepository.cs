using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using UpcomingGames.Database;
using UpcomingGamesBackend.Model.Entities;
using UpcomingGamesBackend.Model.Interfaces;

namespace UpcomingGames.API.Repositories
{
	public class GameRepository : IGameRepository
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

		public Task<int> GetAllItemsCount()
		{
			return _dbContext.Games.CountAsync();
		}

		public Task<List<GameEntity>> GetAll(int page, int pageSize)
		{
			page = Math.Max(page, 1);
			pageSize = Math.Max(pageSize, 1);
			
			return _dbContext.Games.OrderBy(entity => entity.Id).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
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