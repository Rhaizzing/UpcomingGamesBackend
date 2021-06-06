using System.Collections.Generic;
using UpcomingGames.Database.Entities;

namespace UpcomingGames.Database.DTO
{
	public record FullGameDto(GameEntity Game, IEnumerable<PlatformEntity> Platforms, IEnumerable<GenreEntity> Genres, 
		IEnumerable<ThemeEntity> Themes, IEnumerable<CompanyEntity> Companies);
}