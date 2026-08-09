using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.BusinessLayer.Cross_Cutting;

namespace Backend.BusinessLayer.Users
{
    internal class UserFacade
    {
        private Dictionary<string, UserBL> users;
        private AuthenticationFacade authFacade;
    }
}
