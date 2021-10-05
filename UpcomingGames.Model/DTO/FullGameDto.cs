using UpcomingGamesBackend.Model.Entities;

namespace UpcomingGamesBackend.Model.DTO
{
	public record FullGameDto(GameEntity Game, IEnumerable<PlatformEntity>? Platforms, IEnumerable<GenreEntity>? Genres, 
		IEnumerable<ThemeEntity>? Themes, IEnumerable<CompanyEntity>? Companies);
}