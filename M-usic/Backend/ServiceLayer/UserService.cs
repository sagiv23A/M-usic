using Backend.BusinessLayer.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.ServiceLayer
{
    internal class UserService
    {
        private UserFacade uf;

        internal UserService(UserFacade userFacade)
        {
            if (userFacade == null)
                throw new ArgumentNullException("UserFacade cant be null");
            this.uf = userFacade;
        }
    }
}
