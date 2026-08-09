using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BusinessLayer.Song
{
    internal interface IYoutubeRepository
    {
        Task<Stream> GetAudioStreamAsync(string songTitleAndArtist);
       
    }
}
