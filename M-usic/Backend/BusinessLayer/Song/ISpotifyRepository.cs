using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BusinessLayer.Song
{
    public interface ISpotifyRepository
    {
        Task<IEnumerable<SongBL>> GetSongInfoAsync(string query);
      
    }
}
