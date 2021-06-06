using System.Collections.Generic;
using System.Threading.Tasks;
using UpcomingGames.Database.DTO;
using UpcomingGames.Database.Entities;

namespace UpcomingGames.Sources.Interfaces
{
	public interface IGameSource<T>
	{
		Task<FullGameDto> GetOne(T gameQuery);
		Task<IEnumerable<FullGameDto>> Search(string searchQuery);
		Task<IEnumerable<FullGameDto>> GetAll(int page, int itemsPerPage);
	}
}