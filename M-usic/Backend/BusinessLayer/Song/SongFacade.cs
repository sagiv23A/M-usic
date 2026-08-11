using Backend.BusinessLayer.Cross_Cutting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BusinessLayer.Song
{
    internal class SongFacade
    {
        private Dictionary<string, SongBL> songsCache;
        private AuthenticationFacade authoFacade;

        public SongFacade(AuthenticationFacade authFacade)
        {
            this.authoFacade = authFacade;
            songsCache = new Dictionary<string, SongBL>(StringComparer.OrdinalIgnoreCase);
        }

        public Task<List<SongBL>> SearchSongs(string query)
        {
            throw new NotImplementedException();
        } 

        public Task<Stream> GetAudioStream(string songId)
        {
            throw new NotImplementedException();
        }
    }
}
