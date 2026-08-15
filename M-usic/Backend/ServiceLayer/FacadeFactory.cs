using Backend.BusinessLayer.Cross_Cutting;
using Backend.BusinessLayer.Song;
using Backend.BusinessLayer.Users;
using Backend.Infrastructure;
using log4net;
using log4net.Config;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Backend.ServiceLayer
{
    public class FacadeFactory
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private UserService US;
        private SongService SS;


        public UserService User
        {
            get => US;
        }

        public SongService Song
        {
            get => SS;
        }

        public FacadeFactory()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetExecutingAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            log.Info("log4net configuration has been loaded successfully in FacadeFactory.");
            AuthenticationFacade authoFacade = new AuthenticationFacade();
            string spotifyClientId = "fd57b14d63fc4ba0a68a02aa45056c66";
            string spotifyClientSecret = "dea4c5f9c8a6497a975a18ab481eafc3";
            SpotifyRepository spotifyRepo = new SpotifyRepository(spotifyClientId, spotifyClientSecret);
            YoutubeRepository youtubeRepo = new YoutubeRepository();
            UserFacade tempUser = new UserFacade(authoFacade);
            SongFacade tempSong = new SongFacade(spotifyRepo, youtubeRepo,authoFacade);
            US = new UserService(tempUser);
            SS = new SongService(tempSong);
            log.Info("FacadeFactory and all services (User, Board, Columns, Task) have been initialized successfully.");

        }
    }
}
