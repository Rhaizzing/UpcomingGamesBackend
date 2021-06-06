using System.Threading.Tasks;
using UpcomingGames.Database.Entities;

namespace UpcomingGames.Sources.Interfaces
{
	public interface IGameSource
	{
		Task<Game> GetById(long id);
		Task<Game> SearchByName(string name);
	}
}