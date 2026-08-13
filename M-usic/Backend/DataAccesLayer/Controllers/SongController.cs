using Backend.BusinessLayer.Exceptions;
using Backend.DataAccesLayer.Dto_s;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DataAccesLayer.Controllers
{
    internal class SongController
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private const string SongsTableName = "Songs";
        private readonly string connectionString;
        private readonly string tableName;

        public SongController()
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Music.db"));
            this.connectionString = $"Data Source={path}; Version=3; BusyTimeout=5000;";
            this.tableName = SongsTableName;
        }

        internal bool Insert(SongDto song)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {tableName} ({SongDto.id} ,{SongDto.songId} ,{SongDto.userEmail} ,{SongDto.title} ,{SongDto.artist} ,{SongDto.duration} ,{SongDto.coverUrl}) " +
                        $"VALUES (@idVal,@songIdWordVal,@userEmailVal,@titleVal,@artistVal,@durationVal,@coverUrlVal);";

                    SQLiteParameter idParam = new SQLiteParameter(@"idVal", song.Id);
                    SQLiteParameter songIDParam = new SQLiteParameter(@"songIdVal", song.SongId);
                    SQLiteParameter userEmailParam = new SQLiteParameter("@userEmailVal", song.UserEmail);
                    SQLiteParameter titleParam = new SQLiteParameter("@titleVal", song.Title);
                    SQLiteParameter artistParam = new SQLiteParameter("@artistVal", song.Artist);
                    SQLiteParameter durationParam = new SQLiteParameter("@durationVal", song.Duration);
                    SQLiteParameter coverUrlPararm = new SQLiteParameter("@coverUrlVal", song.CoverUrl);

                    command.Parameters.Add(idParam);
                    command.Parameters.Add(songIDParam);
                    command.Parameters.Add(userEmailParam);
                    command.Parameters.Add(titleParam);
                    command.Parameters.Add(artistParam);
                    command.Parameters.Add(durationParam);
                    command.Parameters.Add(coverUrlPararm);
                    command.Prepare();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    log.Error($"Failed tp Insert song {song.SongId}: {ex.Message}");
                    throw new MusicException("Failed to register user. please try again");
                }
                finally
                {
                    connection.Dispose();
                }
                return res > 0;
            }
        }

        internal bool Delete(SongDto song)
        {
            int res = -1;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteParameter idParam = new SQLiteParameter(@"@idVal", song.Id);
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from {tableName} where [{SongDto.id}]=@idVal"
                };
                try
                {
                    command.Parameters.Add(idParam);
                    connection.Open();
                    command.Prepare();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    log.Error($"Failed to delete song {song.Id}: {ex.Message}");
                    throw new MusicException("Failed to delete the song. Please try again.");
                }
                finally
                {
                    command.Dispose();
                }

            }
            return res > 0;
        }

        internal bool Update(int id, string attributeName, object attributeValue)
        {
            int res = -1;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteParameter idParam = new SQLiteParameter("@idVal", id);
                SQLiteParameter attributeParam = new SQLiteParameter("@attributeVal", attributeValue);
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"update {tableName} set [{attributeName}]=@attributeVal where [{SongDto.id}]=@idVal"
                };
                try
                {
                    command.Parameters.Add(idParam);
                    command.Parameters.Add(attributeParam);
                    connection.Open();
                    command.Prepare();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    log.Error($"Failed to update song {id}: {ex.Message}");
                    throw new MusicException("Failed to update the song. Please try again.");
                }
                finally
                {
                    command.Dispose();
                }
            }
            return res > 0;
        }

        internal List<SongDto> GetAllSongs()
        {
            List<SongDto> songs = new List<SongDto>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand($"SELECT * FROM {SongsTableName}", connection))
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        int idOrdinal = reader.GetOrdinal(SongDto.id);
                        int songIdOrdinal = reader.GetOrdinal(SongDto.songId);
                        int userEmailOrdinal = reader.GetOrdinal(SongDto.userEmail);
                        int titleOrdinal = reader.GetOrdinal(SongDto.title);
                        int artistOrdinal = reader.GetOrdinal(SongDto.artist);
                        int durationOrdinal = reader.GetOrdinal(SongDto.duration);
                        int coverUrlOrdinal = reader.GetOrdinal(SongDto.coverUrl);

                        while (reader.Read())
                        {
                            int id = reader.GetInt32(idOrdinal);
                            string songId = reader.GetString(songIdOrdinal);
                            string userEmail = reader.GetString(userEmailOrdinal);
                            string title = reader.GetString(titleOrdinal);
                            string artist = reader.IsDBNull(artistOrdinal) ? string.Empty : reader.GetString(artistOrdinal);
                            string duration = reader.IsDBNull(durationOrdinal) ? string.Empty : reader.GetString(durationOrdinal);
                            string coverUrl = reader.IsDBNull(coverUrlOrdinal) ? string.Empty : reader.GetString(coverUrlOrdinal);

                            SongDto song = new SongDto(id, songId, userEmail, title, artist, duration, coverUrl);

                            songs.Add(song);
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error($"Failed to retrieve songs: {ex.Message}");
                    throw new MusicException("Failed to retrieve songs. Please try again.");
                }
            }

            return songs;
        }

        internal bool DeleteAllSongs()
        {
            int res = -1;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from {tableName}"
                };
                try
                {
                    connection.Open();
                    command.Prepare();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    log.Error($"Failed to delete all songs: {ex.Message}");
                    throw new MusicException("Failed to delete all songs. Please try again.");
                }
                finally
                {
                    command.Dispose();
                }
            }
            return res >= 0;
        }

    }
}
