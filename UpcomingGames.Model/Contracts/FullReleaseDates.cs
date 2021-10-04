namespace UpcomingGamesBackend.Model.Contracts
{
	public class FullReleaseDates
	{
		public DateOnly? FirstReleaseDate { get; set; }
		public Dictionary<string, DateOnly> Worldwide { get; set; } = new();
		public Dictionary<string, DateOnly>  NorthAmerica { get; set; } = new();
		public Dictionary<string, DateOnly>  Europe { get; set; } = new();
		public Dictionary<string, DateOnly>  Australia { get; set; } = new();
		public Dictionary<string, DateOnly>  NewZealand { get; set; } = new();
		public Dictionary<string, DateOnly>  Japan { get; set; } = new();
		public Dictionary<string, DateOnly>  China { get; set; } = new();
		public Dictionary<string, DateOnly>  Asia { get; set; } = new();
	}
}