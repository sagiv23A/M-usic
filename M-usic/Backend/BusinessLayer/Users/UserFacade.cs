using Backend.BusinessLayer.Cross_Cutting;
using Backend.ServiceLayer;
using IntroSE.Kanban.Backend.BusinessLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BusinessLayer.Users
{
    internal class UserFacade
    {
        private Dictionary<string, UserBL> users;
        private AuthenticationFacade authFacade;

        internal UserFacade(AuthenticationFacade authFacade)
        {
            this.authFacade = authFacade;
            users = new Dictionary<string, UserBL>(StringComparer.OrdinalIgnoreCase);

        }

        public UserBL Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        public bool logout(string email)
        {
            throw new NotImplementedException();
        }

        public UserBL Register(string email, string password)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Validates that the provided email string is not null or empty.
        /// </summary>
        /// <param name="email">The email string to validate.</param>
        /// <exception cref="KanbanException">Thrown if the email string is null or empty.</exception>
        /// <precondition>There is no precondition.</precondition>
        /// <postcondition>Validates successfully if the email contains text.</postcondition>
        private void isNullOrEmptyEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new MusicException("email cant be null");
            }
        }
    }
    }
