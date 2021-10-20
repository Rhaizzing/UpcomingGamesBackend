using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpcomingGames.API.Repositories;
using UpcomingGames.Sources.Interfaces;
using UpcomingGamesBackend.Model.DTO;
using UpcomingGamesBackend.Model.Interfaces;

namespace UpcomingGames.API.Services
{
	public class SyncDatabase<T, I> where  T : IGameSource<I>
	{
		private readonly IGameRepository _repository;
		private readonly T _gameSource;

		public SyncDatabase(IGameRepository repository, T source)
		{
			_repository = repository;
			_gameSource = source;
		}

		public async Task<int> SyncGameDatabase()
		{
			var gamesCount = await _gameSource.GetGamesCount();

			if (gamesCount == 0)
				return 0;

			List<FullGameDto?> games = new();
			
			for(var i = 1; i <= (int)Math.Ceiling(gamesCount / 500.0); i++)
				games.AddRange(await _gameSource.GetAll(i, 500));

			foreach (var game in games.Where(entry => entry is not null))
				await _repository.Add(game!.Game);
			
			return _repository.SaveChanges();
		}

		public async Task<bool> SyncOneGame(I id)
		{
			var game = await _gameSource.GetOne(id);

			if (game is null)
				return false;

			await _repository.Add(game.Game);

			return _repository.SaveChanges() == 1;
		}
	}
}