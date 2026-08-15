using Backend.BusinessLayer.Song;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.ServiceLayer
{
    public class SongSL
    {
        public string title { get; }
        public string duration { get; }
        public List<string> artists { get; }
        public string songId { get; }
        public string coverArtURL { get; }

        public SongSL(string title, string duration, List<string> artists, string songId, string coverArtURL)
        {
            this.title = title;
            this.duration = duration;
            this.artists = artists;
            this.songId = songId;
            this.coverArtURL = coverArtURL;
        }
        public SongSL() { }

        internal SongSL(SongBL songBL)
        {
            this.title = songBL.title;
            this.duration = songBL.duration;
            this.artists = songBL.artists;
            this.songId = songBL.songId;
            this.coverArtURL = songBL.coverArtURL;
        }
    }
}
