using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BusinessLayer.Song
{
    internal class SongBL
    {
        public string title { get; }
        public string duration { get; }
        public List<string> artists { get; }
        public string songId { get; }
        public string coverArtURL { get; }

        public SongBL(string title, string duration, List<string> artists, string songId, string coverArtURL)
        {
            this.title = title;
            this.duration = duration;
            this.artists = artists;
            this.songId = songId;
            this.coverArtURL = coverArtURL;
        }
    }
}
