namespace UpcomingGamesBackend.Model.Contracts
{
	public class ReleaseDates
	{
		public string FirstReleaseDate { get; set; } = "NA";
		public Dictionary<string, string> Worldwide { get; set; } = new Dictionary<string, string>();
		public Dictionary<string, string>  NorthAmerica { get; set; } = new Dictionary<string, string>();
		public Dictionary<string, string>  Europe { get; set; } = new Dictionary<string, string>();
		public Dictionary<string, string>  Australia { get; set; } = new Dictionary<string, string>();
		public Dictionary<string, string>  NewZealand { get; set; } = new Dictionary<string, string>();
		public Dictionary<string, string>  Japan { get; set; } = new Dictionary<string, string>();
		public Dictionary<string, string>  China { get; set; } = new Dictionary<string, string>();
		public Dictionary<string, string>  Asia { get; set; } = new Dictionary<string, string>();
	}
}