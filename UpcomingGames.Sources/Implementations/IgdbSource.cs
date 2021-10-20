using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGDB;
using IGDB.Models;
using UpcomingGames.Sources.Interfaces;
using UpcomingGames.Sources.Utils;
using UpcomingGamesBackend.Model.DTO;

namespace UpcomingGames.Sources.Implementations
{
	public class IgdbSource : IGameSource<long>
	{
		private readonly IGDBClient _client;

		private const string FIELDS =
			"fields id, name, release_dates.*, release_dates.platform.name, cover.image_id, age_ratings.*, aggregated_rating, url, websites.*, genres.name, themes.name, involved_companies.company.name, involved_companies.company.logo.url";

		public IgdbSource(IGDBClient client)
		{
			_client = client;
		}

		public async Task<FullGameDto?> GetOne(long igdbGameId)
		{
			var igdbGames = (await _client.QueryAsync<Game>(IGDBClient.Endpoints.Games, $"{FIELDS}; where id = {igdbGameId};"));

			var igdbGame = igdbGames[0];

			if (igdbGame.ReleaseDates?.Values?.IsGameFullyReleased() ?? false)
				return null;
			
			return new FullGameDto(igdbGame.ConvertFromIgdb(), igdbGame.GetPlatforms(), igdbGame.GetGenres(), 
				igdbGame.GetThemes(), igdbGame.GetCompanies());
		}

		public async Task<IEnumerable<FullGameDto?>> Search(string searchQuery)
		{
			var igdbGames = await _client.QueryAsync<Game>(IGDBClient.Endpoints.Games, $@"search ""{searchQuery}""; {FIELDS};");

			return igdbGames.Select(igdbGame =>
			{
				if (igdbGame.ReleaseDates?.Values?.IsGameFullyReleased() ?? false)
					return null;

				return new FullGameDto(igdbGame.ConvertFromIgdb(), igdbGame.GetPlatforms(), igdbGame.GetGenres(), 
					igdbGame.GetThemes(), igdbGame.GetCompanies());
			});
		}

		public async Task<IEnumerable<FullGameDto?>> GetAll(int page, int itemsPerPage)
		{
			var query =
				$"sort release_dates.date asc; where ((status != 0 & status != 5 & status != 6) | status = null) & (release_dates.date >= {DateTimeOffset.Now.ToUnixTimeSeconds()} | first_release_date >= {DateTimeOffset.Now.ToUnixTimeSeconds()})";
			
			var pagination = $"offset {(page - 1) * itemsPerPage}; limit {itemsPerPage}";
			
			var igdbGames = await _client.QueryAsync<Game>(IGDBClient.Endpoints.Games, $"{FIELDS}; {query}; {pagination};");

			return igdbGames.Select(igdbGame =>
			{
				if (igdbGame.ReleaseDates?.Values?.IsGameFullyReleased() ?? false)
					return null;

				return new FullGameDto(igdbGame.ConvertFromIgdb(), igdbGame.GetPlatforms(), igdbGame.GetGenres(), 
					igdbGame.GetThemes(), igdbGame.GetCompanies());
			});
		}
		
		public async Task<int> GetGamesCount()
		{
			var query = $"where ((status != 0 & status != 5 & status != 6) | status = null) & (release_dates.date >= {DateTimeOffset.Now.ToUnixTimeSeconds()} | first_release_date >= {DateTimeOffset.Now.ToUnixTimeSeconds()});";

			var totalGames = (await _client.CountAsync(IGDBClient.Endpoints.Games, query));

			return totalGames?.Count ?? 0;
		}
	}
}