using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGDB;
using IGDB.Models;
using UpcomingGames.Database.DTO;
using UpcomingGames.Database.Entities;
using UpcomingGames.Sources.Interfaces;
using UpcomingGames.Sources.Utils;

namespace UpcomingGames.Sources.Implementations
{
	public class IgdbSource : IGameSource<long>
	{
		private readonly IGDBClient _client;

		private const string FIELDS =
			"id, name, release_dates.*, release_dates.platform.name, cover.url, age_ratings.*, aggregated_rating, url, websites.*, genres.name, themes.name, involved_companies.company.name, involved_companies.company.logo.url";

		public IgdbSource(IGDBClient client)
		{
			_client = client;
		}

		public async Task<FullGameDto> GetOne(long igdbGameId)
		{
			var igdbGame = (await _client.QueryAsync<Game>(IGDBClient.Endpoints.Games, $"{FIELDS}; where id = {igdbGameId};"))[0];

			if (igdbGame.ReleaseDates?.Values?.IsGameFullyReleased() ?? false)
				return null;
			
			return new FullGameDto(igdbGame.ConvertFromIgdb(), igdbGame.GetPlatforms(), igdbGame.GetGenres(), 
				igdbGame.GetThemes(), igdbGame.GetCompanies());
		}

		public async Task<IEnumerable<FullGameDto>> Search(string searchQuery)
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

		public Task<IEnumerable<FullGameDto>> GetAll(int page, int itemsPerPage)
		{
			throw new NotImplementedException();
		}
	}
}