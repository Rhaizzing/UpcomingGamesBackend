#nullable disable

namespace UpcomingGames.Database.Entities
{
    public partial class GameTheme
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int ThemeId { get; set; }

        public virtual Game Game { get; set; }
        public virtual Theme Theme { get; set; }
    }
}
