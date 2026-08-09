using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BusinessLayer.Users
{
    internal class UserBL
    {
        private string email;
        private string password;

        public UserBL(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
    }
}
