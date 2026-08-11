using Backend.BusinessLayer.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BusinessLayer.Cross_Cutting
{
    internal class AuthenticationFacade
    {
        private Dictionary<string, bool> users;
        
        public AuthenticationFacade()
        {
            users = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
        }

        public bool UserIsLoggedIn(string email)
        {
            throw new NotImplementedException();
        }

        public void Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        public bool IsRegisterd(string email) 
        {
            throw new NotImplementedException();
        }

        public bool Register(string email, string password)
        {
            throw new NotImplementedException();
        }

        public void Logout(string email) 
        {
            throw new NotImplementedException();
        }

    }
}
