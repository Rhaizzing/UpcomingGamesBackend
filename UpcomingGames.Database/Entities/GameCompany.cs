#nullable disable

namespace UpcomingGames.Database.Entities
{
    public partial class GameCompany
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Game Game { get; set; }
    }
}
