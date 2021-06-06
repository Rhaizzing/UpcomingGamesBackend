#nullable disable

namespace UpcomingGames.Database.Entities
{
    public partial class 
        GameGenreEntity
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int GenreId { get; set; }

        public virtual GameEntity GameEntity { get; set; }
        public virtual GenreEntity GenreEntity { get; set; }
    }
}
