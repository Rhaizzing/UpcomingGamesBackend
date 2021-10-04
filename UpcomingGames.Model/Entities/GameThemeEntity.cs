#nullable disable

namespace UpcomingGamesBackend.Model.Entities
{
    public partial class GameThemeEntity
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int ThemeId { get; set; }

        public virtual GameEntity GameEntity { get; set; }
        public virtual ThemeEntity ThemeEntity { get; set; }
    }
}
