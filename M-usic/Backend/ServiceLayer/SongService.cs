using Backend.BusinessLayer.Song;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.ServiceLayer
{
    internal class SongService
    {
        private SongFacade sf;

        internal SongService(SongFacade sf)
        {
            if(sf == null)
            {
                throw new ArgumentNullException("SongFacade cant be null");
            }
            this.sf = sf;
        }

    }
}
