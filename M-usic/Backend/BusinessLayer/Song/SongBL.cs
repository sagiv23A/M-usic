using Backend.DataAccesLayer.Dto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.BusinessLayer.Exceptions;

namespace Backend.BusinessLayer.Song
{
    public class SongBL
    {
        public string title { get; }
        public string duration { get; }
        public List<string> artists { get; }
        public string songId { get; }
        public string coverArtURL { get; }
        private SongDto songDto { get; }    


        public SongBL(string title, string duration, List<string> artists, string songId, string coverArtURL, string email = "")
        {
            this.title = title;
            this.duration = duration;
            this.artists = artists;
            this.songId = songId;
            this.coverArtURL = coverArtURL;
            string artistsAsString = string.Join(", ", this.artists);
            songDto = new SongDto(songId,email , title, artistsAsString, duration, coverArtURL);
            songDto.Insert();
        }

        internal SongBL(SongDto songDto)
        {
            if(songDto == null)
            {
                throw new MusicException("SongDto can't be null");
            }
            this.songDto = songDto;
            this.title = songDto.Title;
            this.duration = songDto.Duration;
            this.artists = string.IsNullOrWhiteSpace(songDto.Artist)
                            ? new List<string>()
                            : songDto.Artist.Split(',').Select(a => a.Trim()).ToList();
            this.songId = songDto.SongId;
            this.coverArtURL = songDto.CoverUrl;
        }
    }
}
