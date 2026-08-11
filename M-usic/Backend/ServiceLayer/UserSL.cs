using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.BusinessLayer.Users;

namespace Backend.ServiceLayer
{
    internal class UserSL
    {
        public string email { get; }
        public string userId { get; }
        public UserSL(string email, string userId)
        {
            this.email = email;
            this.userId = userId;
        }
        public UserSL()
        {

        }
        public UserSL(UserBL user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            this.email = user.Email;
        }
    }
}
