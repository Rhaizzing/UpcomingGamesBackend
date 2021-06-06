using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using IGDB.Models;
using UpcomingGames.Database.Entities;
using UpcomingGames.Database.Models;

namespace UpcomingGames.Sources.Utils
{
	public static class IgdbUtils
	{
		public static bool IsGameFullyReleased(this IEnumerable<ReleaseDate> releaseDates)
		{
			foreach (var releaseDate in releaseDates)
			{
				if (releaseDate.Category is ReleaseDateCategory.YYYYMMMMDD)
				{
					if (releaseDate.Date > DateTimeOffset.Now)
						return false;
				}
				else
				{
					return false;
				}
			}

			return true;
		}

		public static GameEntity ConvertFromIgdb(this Game igdbGame)
		{
			var upcomingGame = new GameEntity();
			var releaseDates = new ReleaseDates();
			var gameUrls = new GameUrls();

			upcomingGame.Name = igdbGame.Name;

			if (igdbGame.FirstReleaseDate.HasValue)
				releaseDates.FirstReleaseDate = igdbGame.FirstReleaseDate.ToString();

			if (igdbGame.ReleaseDates?.Values is not null)
			{
				string releaseDateString;
				foreach (var releaseDate in igdbGame.ReleaseDates.Values)
				{
					releaseDateString = releaseDate.Category switch
					{
						ReleaseDateCategory.YYYYMMMMDD => releaseDate.Date.ToString(),
						ReleaseDateCategory.YYYYMMMM => $"{releaseDate.Month}/{releaseDate.Year}",
						ReleaseDateCategory.YYYYQ1 => $"Q1/{releaseDate.Year}",
						ReleaseDateCategory.YYYYQ2 => $"Q2/{releaseDate.Year}",
						ReleaseDateCategory.YYYYQ3 => $"Q3/{releaseDate.Year}",
						ReleaseDateCategory.YYYYQ4 => $"Q4/{releaseDate.Year}",
						ReleaseDateCategory.YYYY => releaseDate.Year.ToString(),
						ReleaseDateCategory.TBD => "TBD",
						_ => "NA"
					};

					switch (releaseDate.Region)
					{
						case ReleaseDateRegion.Worldwide:
							releaseDates.Worldwide.Add(releaseDate.Platform.Value.Name, releaseDateString);
							break;
						case ReleaseDateRegion.NorthAmerica:
							releaseDates.NorthAmerica.Add(releaseDate.Platform.Value.Name, releaseDateString);
							break;
						case ReleaseDateRegion.Europe:
							releaseDates.Europe.Add(releaseDate.Platform.Value.Name, releaseDateString);
							break;
						case ReleaseDateRegion.Australia:
							releaseDates.Australia.Add(releaseDate.Platform.Value.Name, releaseDateString);
							break;
						case ReleaseDateRegion.NewZealand:
							releaseDates.NewZealand.Add(releaseDate.Platform.Value.Name, releaseDateString);
							break;
						case ReleaseDateRegion.Japan:
							releaseDates.Japan.Add(releaseDate.Platform.Value.Name, releaseDateString);
							break;
						case ReleaseDateRegion.China:
							releaseDates.China.Add(releaseDate.Platform.Value.Name, releaseDateString);
							break;
						case ReleaseDateRegion.Asia:
							releaseDates.Asia.Add(releaseDate.Platform.Value.Name, releaseDateString);
							break;
						default:
							throw new ArgumentOutOfRangeException(nameof(releaseDate.Region));
					}
				}
			}

			upcomingGame.ReleaseDate = JsonSerializer.Serialize(releaseDates);
			
			if(igdbGame.FirstReleaseDate is not null)
				upcomingGame.FullReleaseDate = DateOnly.FromDateTime(igdbGame.FirstReleaseDate.Value.Date);

			if (igdbGame.Cover?.Value is not null)
				upcomingGame.CoverUrl = igdbGame.Cover.Value.Url;

			if (igdbGame.AggregatedRating is not null)
				upcomingGame.Score = igdbGame.AggregatedRating;

			if (igdbGame.AgeRatings?.Values is not null)
				foreach (var ageRating in igdbGame.AgeRatings.Values)
					switch (ageRating.Category)
					{
						case AgeRatingCategory.ESRB:
							upcomingGame.EsrbRating = ageRating.Rating.ToString();
							break;
						case AgeRatingCategory.PEGI:
							upcomingGame.PegiRating = ageRating.Rating.ToString();
							break;
						default:
							throw new ArgumentOutOfRangeException(nameof(ageRating.Category));
					}

			if (igdbGame.Websites?.Values is not null)
				foreach (var site in igdbGame.Websites.Values)
				{
					switch (site.Category)
					{
						case WebsiteCategory.Official:
							gameUrls.Official = site.Url;
							break;
						case WebsiteCategory.Wikia:
							gameUrls.Wikia = site.Url;
							break;
						case WebsiteCategory.Wikipedia:
							gameUrls.Wikipedia = site.Url;
							break;
						case WebsiteCategory.Facebook:
							gameUrls.Facebook = site.Url;
							break;
						case WebsiteCategory.Twitter:
							gameUrls.Twitter = site.Url;
							break;
						case WebsiteCategory.Twitch:
							gameUrls.Twitch = site.Url;
							break;
						case WebsiteCategory.Instagram:
							gameUrls.Instagram = site.Url;
							break;
						case WebsiteCategory.YouTube:
							gameUrls.YouTube = site.Url;
							break;
						case WebsiteCategory.iPhone:
							gameUrls.Iphone = site.Url;
							break;
						case WebsiteCategory.iPad:
							gameUrls.Ipad = site.Url;
							break;
						case WebsiteCategory.Android:
							gameUrls.Android = site.Url;
							break;
						case WebsiteCategory.Steam:
							gameUrls.Steam = site.Url;
							break;
						case WebsiteCategory.Reddit:
							gameUrls.Reddit = site.Url;
							break;
						case WebsiteCategory.Itch:
							gameUrls.Itch = site.Url;
							break;
						case WebsiteCategory.EpicGames:
							gameUrls.EpicGames = site.Url;
							break;
						case WebsiteCategory.GOG:
							gameUrls.Gog = site.Url;
							break;
						case WebsiteCategory.Discord:
							gameUrls.Discord = site.Url;
							break;
						default:
							throw new ArgumentOutOfRangeException(nameof(site.Category));
					}
				}

			upcomingGame.Urls = JsonSerializer.Serialize(gameUrls);

			upcomingGame.IgdbId = igdbGame.Id ?? 0;

			return upcomingGame;
		}

		public static IEnumerable<PlatformEntity> GetPlatforms(this Game igdbGame)
		{
			return igdbGame.Platforms?.Values?.Select(platform =>
				new PlatformEntity
				{
					Name = platform.Name
				}
			);
		}
		
		public static IEnumerable<GenreEntity> GetGenres(this Game igdbGame)
		{
			return igdbGame.Genres?.Values?.Select(genre =>
				new GenreEntity
				{
					Name = genre.Name
				}
			);
		}
		
		public static IEnumerable<ThemeEntity> GetThemes(this Game igdbGame)
		{
			return igdbGame.Themes?.Values?.Select(theme =>
				new ThemeEntity
				{
					Name = theme.Name
				}
			);
		}
		
		public static IEnumerable<CompanyEntity> GetCompanies(this Game igdbGame)
		{
			return igdbGame.InvolvedCompanies?.Values?.Select(company =>
				new CompanyEntity
				{
					Name = company.Company.Value.Name,
					LogoUrl = company.Company.Value.Logo?.Value?.Url
				}
			);
		}
	}
}