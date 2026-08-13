using Backend.BusinessLayer.Song;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Backend.ServiceLayer
{
    internal class SongService
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private SongFacade sf;

        internal SongService(SongFacade sf)
        {
            if(sf == null)
            {
                throw new ArgumentNullException("SongFacade cant be null");
            }
            this.sf = sf;
            log.Info("SongService initialized successfully");
        }

        /// <summary>
        /// Searches for songs in Spotify via SongFacade.
        /// </summary>
        public async Task<string> SearchSongsAsync(string query)
        {
            log.Info($"Search attempt for query: '{query}'");
            try
            {
                var songs = await sf.SearchSongs(query);
                Response res = new Response(null, songs);
                log.Info($"Search completed successfully for query: '{query}'");
                return JsonSerializer.Serialize(res);
            }
            catch (Exception ex)
            {
                log.Error($"Error during song search for query '{query}': {ex.Message}");
                return ErrResponse(ex.Message);
            }
        }

        /// <summary>
        /// Fetches the audio stream for a given song title and artist from YouTube.
        /// </summary>
        public async Task<AudioStreamResult?> GetAudioStreamAsync(string songTitleAndArtist)
        {
            log.Info($"Audio stream request for: '{songTitleAndArtist}'");
            try
            {
                Stream stream = await sf.GetAudioStream(songTitleAndArtist);
                log.Info($"Audio stream fetched successfully for: '{songTitleAndArtist}'");
                return new AudioStreamResult(stream, "audio/mpeg");
            }
            catch (Exception ex)
            {
                log.Error($"Error fetching audio stream for '{songTitleAndArtist}': {ex.Message}");
                return null;
            }
        }

        private string ErrResponse(string errMessage)
        {
            Response errorR = new Response(errMessage, null);
            return JsonSerializer.Serialize(errorR);
        }
    }

}

