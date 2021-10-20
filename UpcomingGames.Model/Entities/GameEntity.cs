using NpgsqlTypes;

#nullable disable

namespace UpcomingGamesBackend.Model.Entities
{
    public partial class GameEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ReleaseDate { get; set; }
        public string FullReleaseDate { get; set; }
        public string CoverUrl { get; set; }
        public double? Score { get; set; }
        public string EsrbRating { get; set; }
        public string PegiRating { get; set; }
        public bool IsReleased { get; set; }
        public string Urls { get; set; }
        public long IgdbId { get; set; }
        
        public NpgsqlTsVector SearchVector { get; set; }
    }
}
