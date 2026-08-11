using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BusinessLayer.Song
{
    internal interface ISpotifyRepository
    {
        Task<List<SongBL>> GetSongInfoAsync(string query);
      
    }
}
