using System.Threading.Tasks;
using IGDB;
using Microsoft.AspNetCore.Mvc;
using UpcomingGames.API.Repositories;
using UpcomingGames.API.Services;
using UpcomingGames.Sources.Implementations;

namespace UpcomingGames.API.Controllers
{
	[ApiController]
	[Route("api/v1/master")]
	public class MasterController : ControllerBase
	{
		private SyncDatabase<IgdbSource, long> _igdbSource;

		public MasterController(GameRepository repository, IGDBClient igdbClient)
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
	}
}