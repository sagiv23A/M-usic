using Backend.BusinessLayer.Song;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Backend.BusinessLayer.Exceptions;

namespace Backend.ServiceLayer
{
    public class SongService
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
        /// <summary>
        /// Loads all users from the persistence layer.
        /// </summary>
        /// <returns>An empty response if successful, otherwise an error response.</returns>
        /// <precondition> None. </precondition>
        /// <postcondition> All users are loaded into the system state. </postcondition>
        public string LoadData()
        {
            log.Info("Attempting to load all users");
            Response res = null;
            try
            {
                sf.SelectAllSongs();
                res = new Response(null, null);
                log.Info("all users loaded successfully.");
                return JsonSerializer.Serialize(res);
            }
            catch (MusicException kex)
            {
                log.Warn("Failed to load all users. Reason: " + kex.Message);
                return ErrResponse(kex.Message);
            }
            catch (Exception err)
            {
                log.Error("Failed to load all users. Reason: " + err.Message);
                return ErrResponse(err.Message);
            }
        }

        /// <summary>
        /// Deletes all user data from the system.
        /// </summary>
        /// <returns>An empty response if successful, otherwise an error response.</returns>
        /// <precondition> None. </precondition>
        /// <postcondition> All user data is cleared. </postcondition>
        public string DeleteData()
        {
            log.Info("Attempting to delete all users");
            Response res = null;
            try
            {
                sf.DeleteAllSongs();
                res = new Response(null, null);
                log.Info("all users deleted successfully.");
                return JsonSerializer.Serialize(res);
            }
            catch (MusicException kex)
            {
                log.Warn("Failed to delete all users. Reason: " + kex.Message);
                return ErrResponse(kex.Message);
            }
            catch (Exception err)
            {
                log.Error("Failed to delete all users. Reason: " + err.Message);
                return ErrResponse(err.Message);
            }
        }

        private string ErrResponse(string errMessage)
        {
            Response errorR = new Response(errMessage, null);
            return JsonSerializer.Serialize(errorR);
        }
    }

}

