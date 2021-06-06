#nullable disable

namespace UpcomingGames.Database.Entities
{
    public partial class 
        GamePlatformEntity
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int PlatformId { get; set; }

        public virtual GameEntity GameEntity { get; set; }
        public virtual PlatformEntity PlatformEntity { get; set; }
    }
}
