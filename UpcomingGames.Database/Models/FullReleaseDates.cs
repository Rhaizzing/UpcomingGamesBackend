using System;
using System.Collections.Generic;

namespace UpcomingGames.Database.Models
{
	public class FullReleaseDates
	{
		public DateOnly? FirstReleaseDate { get; set; }
		public Dictionary<string, DateOnly> Worldwide { get; set; } = new Dictionary<string, DateOnly>();
		public Dictionary<string, DateOnly>  NorthAmerica { get; set; } = new Dictionary<string, DateOnly>();
		public Dictionary<string, DateOnly>  Europe { get; set; } = new Dictionary<string, DateOnly>();
		public Dictionary<string, DateOnly>  Australia { get; set; } = new Dictionary<string, DateOnly>();
		public Dictionary<string, DateOnly>  NewZealand { get; set; } = new Dictionary<string, DateOnly>();
		public Dictionary<string, DateOnly>  Japan { get; set; } = new Dictionary<string, DateOnly>();
		public Dictionary<string, DateOnly>  China { get; set; } = new Dictionary<string, DateOnly>();
		public Dictionary<string, DateOnly>  Asia { get; set; } = new Dictionary<string, DateOnly>();
	}
}