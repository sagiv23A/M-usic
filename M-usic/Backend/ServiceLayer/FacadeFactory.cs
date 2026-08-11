using Backend.BusinessLayer.Cross_Cutting;
using Backend.BusinessLayer.Song;
using Backend.BusinessLayer.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.ServiceLayer
{
    internal class FacadeFactory
    {
        private UserService US;
        private SongService SS;

        public FacadeFactory()
        {
            AuthenticationFacade authoFacade = new AuthenticationFacade();
            UserFacade tempUser = new UserFacade(authoFacade);
            SongFacade tempSong = new SongFacade(authoFacade);
            US = new UserService(tempUser);
            SS = new SongService(tempSong);
        }
    }
}
