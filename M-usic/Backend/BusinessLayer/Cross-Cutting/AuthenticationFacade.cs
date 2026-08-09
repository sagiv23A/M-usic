using Backend.BuisnessLayer.Users;
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
    }
}
