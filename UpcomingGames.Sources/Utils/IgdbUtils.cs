using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using IGDB;
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

		private static void AddDate(Dictionary<string, string> dictionary, string platform, string humanDate)
		{
			if (dictionary.ContainsKey(platform))
			{
				dictionary[platform] = humanDate;
				return;
			}
			
			dictionary.Add(platform, humanDate);
		}

		private static void AddFullDate(Dictionary<string, DateOnly> dictionary, string platform, DateTimeOffset unix)
		{
			var fullDate = DateOnly.FromDateTime(unix.Date);
			
			if (dictionary.ContainsKey(platform))
			{
				dictionary[platform] = fullDate;
				return;
			}
			
			dictionary.Add(platform, fullDate);
		}
		
		public static GameEntity ConvertFromIgdb(this Game igdbGame)
		{
			var upcomingGame = new GameEntity();
			var releaseDates = new ReleaseDates();
			var fullReleaseDates = new FullReleaseDates();
			var gameUrls = new GameUrls();

			upcomingGame.Name = igdbGame.Name;

			if (igdbGame.FirstReleaseDate.HasValue)
			{
				fullReleaseDates.FirstReleaseDate = DateOnly.FromDateTime(igdbGame.FirstReleaseDate.Value.Date);
				releaseDates.FirstReleaseDate = igdbGame.FirstReleaseDate.ToString();
			}

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
							AddDate(releaseDates.Worldwide,releaseDate.Platform.Value.Name, releaseDateString);
							if(releaseDate.Date is not null)
								AddFullDate(fullReleaseDates.Worldwide, releaseDate.Platform.Value.Name, releaseDate.Date.Value);
							break;
						case ReleaseDateRegion.NorthAmerica:
							AddDate(releaseDates.NorthAmerica,releaseDate.Platform.Value.Name, releaseDateString);
							if(releaseDate.Date is not null)
								AddFullDate(fullReleaseDates.NorthAmerica, releaseDate.Platform.Value.Name, releaseDate.Date.Value);
							break;
						case ReleaseDateRegion.Europe:
							AddDate(releaseDates.Europe,releaseDate.Platform.Value.Name, releaseDateString);
							if(releaseDate.Date is not null)
								AddFullDate(fullReleaseDates.Europe, releaseDate.Platform.Value.Name, releaseDate.Date.Value);
							break;
						case ReleaseDateRegion.Australia:
							AddDate(releaseDates.Australia,releaseDate.Platform.Value.Name, releaseDateString);
							if(releaseDate.Date is not null)
								AddFullDate(fullReleaseDates.Australia, releaseDate.Platform.Value.Name, releaseDate.Date.Value);
							break;
						case ReleaseDateRegion.NewZealand:
							AddDate(releaseDates.NewZealand,releaseDate.Platform.Value.Name, releaseDateString);
							if(releaseDate.Date is not null)
								AddFullDate(fullReleaseDates.NewZealand, releaseDate.Platform.Value.Name, releaseDate.Date.Value);
							break;
						case ReleaseDateRegion.Japan:
							AddDate(releaseDates.Japan,releaseDate.Platform.Value.Name, releaseDateString);
							if(releaseDate.Date is not null)
								AddFullDate(fullReleaseDates.Japan, releaseDate.Platform.Value.Name, releaseDate.Date.Value);
							break;
						case ReleaseDateRegion.China:
							AddDate(releaseDates.China,releaseDate.Platform.Value.Name, releaseDateString);
							if(releaseDate.Date is not null)
								AddFullDate(fullReleaseDates.China, releaseDate.Platform.Value.Name, releaseDate.Date.Value);
							break;
						case ReleaseDateRegion.Asia:
							AddDate(releaseDates.Asia,releaseDate.Platform.Value.Name, releaseDateString);
							if(releaseDate.Date is not null)
								AddFullDate(fullReleaseDates.Asia, releaseDate.Platform.Value.Name, releaseDate.Date.Value);
							break;
						default:
							throw new ArgumentOutOfRangeException(nameof(releaseDate.Region));
					}
				}
			}

			upcomingGame.ReleaseDate = JsonSerializer.Serialize(releaseDates);
			upcomingGame.FullReleaseDate = JsonSerializer.Serialize(fullReleaseDates);

			if (igdbGame.Cover?.Value is not null)
				upcomingGame.CoverUrl = $"https:{ImageHelper.GetImageUrl(igdbGame.Cover.Value.ImageId, ImageSize.CoverBig, true)}";

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