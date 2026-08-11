using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode;
using Backend.BusinessLayer.Song;

namespace Backend.Infrastracture
{
    internal class YoutubeRepository : IYoutubeRepository
    {
        private readonly YoutubeClient _ytClient;

        public YoutubeRepository(YoutubeClient ytClient)
        {
            _ytClient = ytClient;
        }

        public async Task<Stream> GetAudioStreamUrlAsync(string songTitleAndArtist)
        {
            throw new NotImplementedException();
        }
    }
}
