using Backend.BusinessLayer.Cross_Cutting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BusinessLayer.Song
{
    public class SongFacade
    {
        private Dictionary<string, SongBL> songsCache;
        private AuthenticationFacade authoFacade;
        private readonly ISpotifyRepository spotifyRepository;
        private readonly IYoutubeRepository youtubeRepository;

        public SongFacade(AuthenticationFacade authFacade)
        {
            this.authoFacade = authFacade;
            songsCache = new Dictionary<string, SongBL>(StringComparer.OrdinalIgnoreCase);
        }

        public SongFacade(ISpotifyRepository spotifyRepository, IYoutubeRepository youtubeRepository, AuthenticationFacade authFacade = null)
        {
            if(spotifyRepository == null)
            {
                throw new ArgumentNullException(nameof(spotifyRepository));
            }
            if (youtubeRepository == null)
            {
                throw new ArgumentNullException(nameof(youtubeRepository));
            }
            this.spotifyRepository = spotifyRepository;
            this.youtubeRepository = youtubeRepository;
            this.authoFacade = authFacade;
            songsCache = new Dictionary<string, SongBL>(StringComparer.OrdinalIgnoreCase);
        }   

        public async Task<IEnumerable<SongBL>> SearchSongs(string query)
        {
            if(string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException("Query cannot be null or whitespace.", nameof(query));
            }
            return await spotifyRepository.GetSongInfoAsync(query);
        } 

        public async Task<Stream> GetAudioStream(string songId)
        {
            if(string.IsNullOrWhiteSpace(songId))
            {
                throw new ArgumentException("Song ID cannot be null or whitespace.", nameof(songId));
            }
            return await youtubeRepository.GetAudioStreamAsync(songId);
        }
    }
}
