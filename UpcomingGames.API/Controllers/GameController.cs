using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UpcomingGames.API.Repositories;
using UpcomingGames.Database.Models;

namespace UpcomingGames.API.Controllers
{
	[ApiController]
	[Route("api/v1/game")]
	public class GameController : ControllerBase
	{
		private readonly GameRepository _repository;

		public GameController(GameRepository repository)
		{
			_repository = repository;
		}

		public record DisplayGame(int Id,
			string Name,
			ReleaseDates ReleaseDate,
			FullReleaseDates FullReleaseDate,
			string CoverUrl,
			double? Score,
			string EsrbRating,
			string PegiRating,
			bool IsReleased,
			GameUrls Urls,
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
				JsonSerializer.Deserialize<ReleaseDates>(game.ReleaseDate),
				JsonSerializer.Deserialize<FullReleaseDates>(game.FullReleaseDate),
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
		public async Task<IActionResult> GetAllGames()
		{
			var games = await _repository.GetAll();

			if (!games.Any())
				return NotFound();

			return Ok(games.Select(game => new DisplayGame(
				game.Id,
				game.Name,
				JsonSerializer.Deserialize<ReleaseDates>(game.ReleaseDate),
				JsonSerializer.Deserialize<FullReleaseDates>(game.FullReleaseDate),
				game.CoverUrl,
				game.Score,
				game.EsrbRating,
				game.PegiRating,
				game.IsReleased,
				JsonSerializer.Deserialize<GameUrls>(game.Urls),
				game.IgdbId
			)));
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
				JsonSerializer.Deserialize<ReleaseDates>(game.ReleaseDate),
				JsonSerializer.Deserialize<FullReleaseDates>(game.FullReleaseDate),
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