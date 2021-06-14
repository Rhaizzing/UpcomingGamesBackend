using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using IGDB;

namespace UpcomingGames.Sources.Utils
{
	public class JsonTokenStore : ITokenStore
	{
		private readonly string _jsonPath;
		private TwitchAccessToken _inMemoryToken;

		public JsonTokenStore(string jsonPath)
		{
			_jsonPath = jsonPath;
		}

		public Task<TwitchAccessToken> GetTokenAsync()
		{
			if (_inMemoryToken is not null)
				return Task.FromResult(_inMemoryToken);

			if (!File.Exists(_jsonPath))
				return Task.FromResult(new TwitchAccessToken());

			var jsonText = File.ReadAllText(_jsonPath);
			_inMemoryToken = JsonSerializer.Deserialize<TwitchAccessToken>(jsonText);

			return Task.FromResult(_inMemoryToken);
		}

		public Task<TwitchAccessToken> StoreTokenAsync(TwitchAccessToken token)
		{
			var jsonText = JsonSerializer.Serialize(token);
			File.WriteAllText(_jsonPath, jsonText);

			return Task.FromResult(token);
		}
	}
}