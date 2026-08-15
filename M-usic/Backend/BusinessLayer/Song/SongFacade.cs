using Backend.BusinessLayer.Cross_Cutting;
using Backend.BusinessLayer.Users;
using Backend.DataAccesLayer.Controllers;
using Backend.DataAccesLayer.Dto_s;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Backend.BusinessLayer.Song
{
    public class SongFacade
    {
        private readonly Dictionary<string, SongBL> songsCache;
        private readonly AuthenticationFacade authoFacade;
        private readonly ISpotifyRepository spotifyRepository;
        private readonly IYoutubeRepository youtubeRepository;

        public SongFacade(ISpotifyRepository spotifyRepository, IYoutubeRepository youtubeRepository, AuthenticationFacade authFacade = null)
        {
            this.spotifyRepository = spotifyRepository ?? throw new ArgumentNullException(nameof(spotifyRepository));
            this.youtubeRepository = youtubeRepository ?? throw new ArgumentNullException(nameof(youtubeRepository));
            this.authoFacade = authFacade;
            this.songsCache = new Dictionary<string, SongBL>(StringComparer.OrdinalIgnoreCase);
        }

        public async Task<IEnumerable<SongBL>> SearchSongs(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException("Query cannot be null or whitespace.", nameof(query));
            }
            return await spotifyRepository.GetSongInfoAsync(query);
        }

        public async Task<Stream> GetAudioStream(string songId)
        {
            if (string.IsNullOrWhiteSpace(songId))
            {
                throw new ArgumentException("Song ID cannot be null or whitespace.", nameof(songId));
            }
            return await youtubeRepository.GetAudioStreamAsync(songId);
        }

        internal void SelectAllSongs()
        {
            songsCache.Clear();
            SongController sc = new SongController();
            List<SongDto> allSongs = sc.GetAllSongs();
            foreach (SongDto song in allSongs)
            {
                SongBL songbl = new SongBL(song);
                songsCache[song.SongId] = songbl;
            }
        }

        internal void DeleteAllSongs()
        {
            SongController sc = new SongController();
            sc.DeleteAllSongs();
            songsCache.Clear();
        }
    }
}