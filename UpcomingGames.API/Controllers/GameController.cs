using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UpcomingGames.API.Repositories;
using UpcomingGames.API.Utils;
using UpcomingGamesBackend.Model.Contracts;
using UpcomingGamesBackend.Model.DTO;

namespace UpcomingGames.API.Controllers
{
	[ApiController]
	[Route("api/v1/game")]
	public class GameController : ControllerBase
	{
		private readonly GameRepository _repository;

		readonly JsonSerializerOptions _serializeOptions = new()
		{
			WriteIndented = true,
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
			Converters =
			{
				new DateOnlyJsonConverter()
			}
		};

		public GameController(GameRepository repository)
		{
			_repository = repository;
		}

		private record DisplayGame(int Id,
			string Name,
			ReleaseDates? ReleaseDate,
			FullReleaseDates? FullReleaseDate,
			string CoverUrl,
			double? Score,
			string EsrbRating,
			string PegiRating,
			bool IsReleased,
			GameUrls? Urls,
			long IgdbId);

		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById(int id)
		{
			var game = await _repository.GetById(id);

			if (game is null)
				return NotFound();

			return Ok(new DisplayGame(
				game.Id,
				game.Name,
				JsonSerializer.Deserialize<ReleaseDates>(game.ReleaseDate, _serializeOptions),
				JsonSerializer.Deserialize<FullReleaseDates>(game.FullReleaseDate, _serializeOptions),
				game.CoverUrl,
				game.Score,
				game.EsrbRating,
				game.PegiRating,
				game.IsReleased,
				JsonSerializer.Deserialize<GameUrls>(game.Urls),
				game.IgdbId
			));
		} 
		
		[HttpGet]
		public async Task<IActionResult> GetAllGames(int page, int pageSize)
		{
			var totalGames = await _repository.GetAllItemsCount();
			var games = await _repository.GetAll(page, pageSize);

			if (!games.Any())
				return NotFound();

			return Ok(new PaginatedResource<IEnumerable<DisplayGame>>
			{
				Page = page,
				PageSize = pageSize,
				TotalPages = totalGames / pageSize,
				TotalItems = totalGames,
				Data = games.Select(game => new DisplayGame(
					game.Id,
					game.Name,
					JsonSerializer.Deserialize<ReleaseDates>(game.ReleaseDate, _serializeOptions),
					JsonSerializer.Deserialize<FullReleaseDates>(game.FullReleaseDate, _serializeOptions),
					game.CoverUrl,
					game.Score,
					game.EsrbRating,
					game.PegiRating,
					game.IsReleased,
					JsonSerializer.Deserialize<GameUrls>(game.Urls),
					game.IgdbId
				))
			});
		} 
		
		[HttpGet("search/{query}")]
		public async Task<IActionResult> GetAllGames(string query)
		{
			var games = await _repository.SearchByName(query);

			if (!games.Any())
				return NotFound();

			return Ok(games.Select(game => new DisplayGame(
				game.Id,
				game.Name,
				JsonSerializer.Deserialize<ReleaseDates>(game.ReleaseDate, _serializeOptions),
				JsonSerializer.Deserialize<FullReleaseDates>(game.FullReleaseDate, _serializeOptions),
				game.CoverUrl,
				game.Score,
				game.EsrbRating,
				game.PegiRating,
				game.IsReleased,
				JsonSerializer.Deserialize<GameUrls>(game.Urls),
				game.IgdbId
			)));
		} 
	}
}