using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Backend.BusinessLayer.Song;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace Backend.Infrastructure
{
    public class YoutubeRepository : IYoutubeRepository
    {
        private readonly YoutubeClient _ytClient;

        public YoutubeRepository() : this(new YoutubeClient())
        {
        }

        public YoutubeRepository(YoutubeClient ytClient)
        {
            _ytClient = ytClient ?? throw new ArgumentNullException(nameof(ytClient));
        }

        public async Task<Stream> GetAudioStreamAsync(string songTitleAndArtist)
        {
            if (string.IsNullOrWhiteSpace(songTitleAndArtist))
            {
                return null;
            }
            YoutubeExplode.Search.VideoSearchResult searchResult = null;
            await foreach (var video in _ytClient.Search.GetVideosAsync(songTitleAndArtist))
            {
                searchResult = video;
                break;
            }
            if (searchResult == null)
            {
                throw new Exception($"No YouTube video found for query: '{songTitleAndArtist}'");
            }
            var streamManifest = await _ytClient.Videos.Streams.GetManifestAsync(searchResult.Id);
            var audioStreamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
            if (audioStreamInfo == null)
            {
                throw new Exception($"No audio stream available for video: '{searchResult.Title}'");
            }
            return await _ytClient.Videos.Streams.GetAsync(audioStreamInfo);
        }
    }
}