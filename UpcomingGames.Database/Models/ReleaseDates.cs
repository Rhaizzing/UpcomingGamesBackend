using System;
using System.Collections.Generic;

namespace UpcomingGames.Database.Models
{
	public class ReleaseDates
	{
		public string FirstReleaseDate { get; set; } = "NA";
		public Dictionary<string, string> Worldwide { get; set; }
		public Dictionary<string, string>  NorthAmerica { get; set; }
		public Dictionary<string, string>  Europe { get; set; }
		public Dictionary<string, string>  Australia { get; set; }
		public Dictionary<string, string>  NewZealand { get; set; }
		public Dictionary<string, string>  Japan { get; set; }
		public Dictionary<string, string>  China { get; set; }
		public Dictionary<string, string>  Asia { get; set; }
	}
}