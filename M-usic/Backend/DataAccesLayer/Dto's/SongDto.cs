using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.DataAccesLayer.Controllers;

namespace Backend.DataAccesLayer.Dto_s
{
    internal class SongDto
    {
        internal const string id = "Id";
        internal const string songId = "SongId";
        internal const string userEmail = "UserEmail";
        internal const string title = "Title";
        internal const string artist = "Artist";
        internal const string duration = "Duration";
        internal const string coverUrl = "CoverUrl";
        private int _id;
        private string _songId;
        private string _userEmail;
        private string _title;
        private string _artist;
        private string _duration;
        private string _coverUrl;
        private SongController songController;
        private bool isPersisted;

        internal bool IsPersisted
        {
            get => isPersisted;
            set => isPersisted = value;
        }

        internal int Id
        {
            get => _id;
            set
            {
                if (isPersisted)
                    songController.Update(Id, id, value);
                _id = value;
            }
        }

        internal string SongId
        {
            get => _songId;
            set
            {
                if (isPersisted)
                    songController.Update(Id, songId, value);
                _songId = value;
            }
        }
        internal string UserEmail
        {
            get => _userEmail;
            set
            {
                if (isPersisted)
                    songController.Update(Id, userEmail, value);
                _userEmail = value;
            }
        }
        internal string Title
            {
            get => _title;
            set
            {
                if (isPersisted)
                    songController.Update(Id, title, value);
                _title = value;
            }
        }
        internal string Artist
        {
            get => _artist;
            set
            {
                if (isPersisted)
                    songController.Update(Id, artist, value);
                _artist = value;
            }
        }
        internal string Duration
        {
            get => _duration;
            set
            {
                if (isPersisted)
                    songController.Update(Id, duration, value);
                _duration = value;
            }
        }
        internal string CoverUrl
        {
            get => _coverUrl;
            set
            {
                if (isPersisted)
                    songController.Update(Id, coverUrl, value);
                _coverUrl = value;
            }
        }

        public SongDto(string songId, string userEmail, string title, string artist, string duration, string coverUrl)
             : this(-1, songId, userEmail, title, artist, duration, coverUrl)
        {
            isPersisted = false; 
        }

        public SongDto(int id, string songId, string userEmail, string title, string artist, string duration, string coverUrl)
        {
            Id = id;
            SongId = songId;
            UserEmail = userEmail;
            Title = title;
            Artist = artist;
            Duration = duration;
            CoverUrl = coverUrl;
            songController = new SongController(); 
            isPersisted = true; 
        }

        internal void Insert()
        {
            songController.Insert(this);
            isPersisted = true;
        }

        internal void Delete()
        {
            songController.Delete(this);
        }
    }
}
