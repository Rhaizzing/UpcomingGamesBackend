using System.Threading.Tasks;
using IGDB;
using Microsoft.AspNetCore.Mvc;
using UpcomingGames.API.Repositories;
using UpcomingGames.API.Services;
using UpcomingGames.Sources.Implementations;
using UpcomingGamesBackend.Model.Interfaces;

namespace UpcomingGames.API.Controllers
{
	[ApiController]
	[Route("api/v1/master")]
	public class MasterController : ControllerBase
	{
		private readonly SyncDatabase<IgdbSource, long> _igdbSource;

		public MasterController(IGameRepository repository, IGDBClient igdbClient)
		{
			_igdbSource = new SyncDatabase<IgdbSource, long>(repository, new IgdbSource(igdbClient));
		}


		[HttpGet("sync/igdb")]
		public async Task<IActionResult> SyncAllGamesFromIgdb()
		{
			var success = await _igdbSource.SyncGameDatabase();

			if(success > 0)
				return Ok($"Synced {success} games!");

			return BadRequest();
		}

		[HttpGet("sync/igdb/{id}")]
		public async Task<IActionResult> SyncOneGameFromIgdb(long id)
		{
			var isSuccess = await _igdbSource.SyncOneGame(id);

			if(isSuccess)
				return Ok($"Synced '{id}' from IGDB!");

			return BadRequest();
		}
	}
}