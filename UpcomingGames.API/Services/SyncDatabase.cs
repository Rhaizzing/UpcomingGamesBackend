using System;
using System.Threading.Tasks;
using UpcomingGames.API.Repositories;
using UpcomingGames.Sources.Interfaces;

namespace UpcomingGames.API.Services
{
	public class SyncDatabase<T, I> where  T : IGameSource<I>
	{
		private readonly GameRepository _repository;
		private readonly T _gameSource;

		public SyncDatabase(GameRepository repository, T source)
		{
			_repository = repository;
			_gameSource = source;
		}

		public async Task<int> SyncGameDatabase()
		{
			var games = await _gameSource.GetAll(1, 500);

			foreach (var game in games)
				await _repository.Add(game.Game);
			

			return _repository.SaveChanges();
		}
	}
}