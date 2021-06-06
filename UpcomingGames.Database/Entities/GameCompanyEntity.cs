#nullable disable

namespace UpcomingGames.Database.Entities
{
    public partial class 
        GameCompanyEntity
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int CompanyId { get; set; }

        public virtual CompanyEntity CompanyEntity { get; set; }
        public virtual GameEntity GameEntity { get; set; }
    }
}
