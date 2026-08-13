using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.BusinessLayer.Song;
using SpotifyAPI.Web;

namespace Backend.Infrastructure
{
    public class SpotifyRepository : ISpotifyRepository
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private SpotifyClient _spotifyClient;

        public SpotifyRepository(SpotifyClient spotifyClient)
        {
            _spotifyClient = spotifyClient ?? throw new ArgumentNullException(nameof(spotifyClient));
        }

        public SpotifyRepository(string clientId, string clientSecret)
        {
            _clientId = clientId ?? throw new ArgumentNullException(nameof(clientId));
            _clientSecret = clientSecret ?? throw new ArgumentNullException(nameof(clientSecret));
        }

        /// <summary>
        /// דואג לחיבור מול ספטיפיי וקבלת Access Token במידה ועדיין לא התחברנו
        /// </summary>
        private async Task AuthenticateAsync()
        {
            if (_spotifyClient != null) return;

            var config = SpotifyClientConfig.CreateDefault();
            var request = new ClientCredentialsRequest(_clientId, _clientSecret);
            var response = await new OAuthClient(config).RequestToken(request);

            _spotifyClient = new SpotifyClient(config.WithToken(response.AccessToken));
        }

        public async Task<IEnumerable<SongBL>> GetSongInfoAsync(string songName)
        {
            if (string.IsNullOrWhiteSpace(songName))
            {
                return new List<SongBL>();
            }

            await AuthenticateAsync();

            var searchRequest = new SearchRequest(SearchRequest.Types.Track, songName);
            var searchResponse = await _spotifyClient.Search.Item(searchRequest);

            List<SongBL> result = new List<SongBL>();

            if (searchResponse?.Tracks?.Items != null)
            {
                foreach (var item in searchResponse.Tracks.Items)
                {
                    List<string> artistNames = new List<string>();
                    foreach (var artist in item.Artists)
                    {
                        artistNames.Add(artist.Name);
                    }
                    SongBL song = new SongBL(
                        item.Name,                           
                        (item.DurationMs / 1000).ToString(),
                        artistNames,                        
                        item.Id,                             
                        item.Album?.Images?.FirstOrDefault()?.Url 
                    );

                    result.Add(song);
                }
            }

            return result;
        }
    }
}