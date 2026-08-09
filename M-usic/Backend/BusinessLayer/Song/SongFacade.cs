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

        public SongFacade()
        {
            songsCache = new Dictionary<string, SongBL>();
        }
    }
}
