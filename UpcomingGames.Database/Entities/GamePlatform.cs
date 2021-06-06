#nullable disable

namespace UpcomingGames.Database.Entities
{
    public partial class GamePlatform
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int PlatformId { get; set; }

        public virtual Game Game { get; set; }
        public virtual Platform Platform { get; set; }
    }
}
