using Backend.BusinessLayer.Cross_Cutting;
using Backend.BusinessLayer.Exceptions;
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

        public UserFacade(AuthenticationFacade authFacade)
        {
            if(authFacade == null)
            {
                throw new ArgumentNullException(nameof(authFacade));
            }
            this.authFacade = authFacade;
            users = new Dictionary<string, UserBL>(StringComparer.OrdinalIgnoreCase);

        }

        public UserBL Login(string email, string password)
        {
            isNullOrEmptyEmail(email);
            if(!users.ContainsKey(email))
            {
                throw new MusicException("User does not exist");
            }
            if(!users[email].LogIn(password))
            {
                throw new MusicException("Invalid password");
            }
            authFacade.Login(email);
            return users[email];
        }

        public bool Logout(string email)
        {
            isNullOrEmptyEmail(email);
            if(!users.ContainsKey(email))
            {
                throw new MusicException("User does not exist");
            }
            authFacade.Logout(email);
            return true;

        }

        public UserBL Register(string email, string password)
        {
            isNullOrEmptyEmail(email);
            if(users.ContainsKey(email))
            {
                throw new MusicException("User already exists");
            }
            UserBL newUser = new UserBL(email, password);
            users.Add(email, newUser);
            authFacade.Register(email);
            return newUser;
        }

        /// <summary>
        /// Validates that the provided email string is not null or empty.
        /// </summary>
        /// <param name="email">The email string to validate.</param>
        /// <exception cref="MusicException">Thrown if the email string is null or empty.</exception>
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
