using System.Collections.Generic;
using System.Threading.Tasks;
using UpcomingGamesBackend.Model.DTO;

namespace UpcomingGames.Sources.Interfaces
{
	public interface IGameSource<T>
	{
		Task<FullGameDto?> GetOne(T gameQuery);
		Task<IEnumerable<FullGameDto?>> Search(string searchQuery);
		Task<IEnumerable<FullGameDto?>> GetAll(int page, int itemsPerPage);
	}
}