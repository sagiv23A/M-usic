using Backend.BusinessLayer.Song;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.BusinessLayer.Users;
using Backend.BusinessLayer.Song;

namespace Backend.Infrastracture
{
    internal class SpotifyRepository : ISpotifyRepository
    {
        private readonly SpotifyClient _spotifyClient;

        public SpotifyRepository(SpotifyClient spotifyClient)
        {
            _spotifyClient = spotifyClient;
        }

        public async Task<List<SongBL>> GetSongInfoAsync(string songName)
        {
           throw new NotImplementedException();
        }
    }
}
