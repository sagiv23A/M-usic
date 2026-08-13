using Backend.DataAccesLayer.Dto_s;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Backend.BusinessLayer.Exceptions;

namespace Backend.DataAccesLayer.Controllers
{
    internal class UserController
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private const string UsersTableName = "Users";
        private readonly string connectionString;
        private readonly string tableName;

        public UserController()
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Music.db"));
            this.connectionString = $"Data Source={path}; Version=3; BusyTimeout=5000;";
            this.tableName = UsersTableName;
        }

        internal bool Insert(UserDto user)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {tableName} ({UserDto.UserEmail} ,{UserDto.UserPassword}) " +
                        $"VALUES (@emailVal,@passWordVal);";

                    SQLiteParameter idParam = new SQLiteParameter(@"emailVal", user.Email);
                    SQLiteParameter titleParam = new SQLiteParameter(@"passWordVal", user.Password);

                    command.Parameters.Add(idParam);
                    command.Parameters.Add(titleParam);
                    command.Prepare();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    log.Error($"Failed tp Insert user {user.Email}: {ex.Message}");
                    throw new MusicException("Failed to register user. please try again");
                }
                finally
                {
                    connection.Dispose();
                }
                return res > 0;
            }
        }

        internal bool Delete(UserDto user) {
            int res = -1;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteParameter idParam = new SQLiteParameter(@"emailVal", user.Email);
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from {tableName} where [{UserDto.UserEmail}]=@emailVal"
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
                    log.Error($"Failed to delete user {user.Email}: {ex.Message}");
                    throw new MusicException("Failed to delete the user. Please try again.");
                }
                finally
                {
                    command.Dispose();
                }

            }
            return res > 0;
        }

        internal bool Update(string email, string attributeName, string attributeValue)
        {
            int res = -1;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteParameter idParam = new SQLiteParameter("@emailVal", email);
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"update {tableName} set [{attributeName}]=@{attributeName} where [{UserDto.UserEmail}] = @emailVal"
                };
                try
                {
                    command.Parameters.Add(new SQLiteParameter(attributeName, attributeValue));
                    command.Parameters.Add(idParam);
                    connection.Open();
                    command.Prepare();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    log.Error($"Failed to update user {email} attribute {attributeName}: {ex.Message}");
                    throw new MusicException("Failed to update user details. Please try again.");
                }
                finally
                {
                    command.Dispose();
                }

            }
            return res > 0;
        }
        /// <summary>
        /// Retrieves all users from the database.
        /// </summary>
        /// <returns>A list of all UserDto objects.</returns>
        /// <precondition> None. </precondition>
        /// <postcondition> All users are returned. </postcondition>
        internal List<UserDto> SelectAllUsers()
        {
            List<UserDto> results = new List<UserDto>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {tableName};";

                try
                {
                    connection.Open();

                    using (SQLiteDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            results.Add(ConvertReaderToObject(dataReader));
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error($"Failed to retrieve all users: {ex.Message}");
                    throw new MusicException("Failed to retrieve users from the system. Please try again later.");
                }
                finally
                {
                    command.Dispose();
                }
            }
            return results;
        }

        /// <summary>
        /// Deletes all users from the database.
        /// </summary>
        /// <returns>True if successful.</returns>
        /// <precondition> None. </precondition>
        /// <postcondition> The user table is empty. </postcondition>
        internal bool DeleteAllUsers()
        {
            int res = -1;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand enableFKCommand = new SQLiteCommand("PRAGMA foreign_keys = ON;", connection);

                SQLiteCommand deleteCommand = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from {tableName};"
                };

                try
                {
                    connection.Open();

                    enableFKCommand.ExecuteNonQuery();

                    deleteCommand.Prepare();
                    res = deleteCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    log.Error($"Failed to delete all users: {ex.Message}");
                    throw new MusicException("Failed to delete all users. Please try again.");
                }
                finally
                {
                    enableFKCommand.Dispose();
                    deleteCommand.Dispose();
                }
            }
            return res > 0;
        }

        /// <summary>
        /// Maps a database row to a UserDto object.
        /// </summary>
        internal UserDto ConvertReaderToObject(SQLiteDataReader reader)
        {
            return new UserDto(reader.GetString(0), reader.GetString(1));
        }
    }
}
