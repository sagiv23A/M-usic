using Backend.BusinessLayer.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.BusinessLayer.Exceptions;

namespace Backend.BusinessLayer.Cross_Cutting
{
    public class AuthenticationFacade
    {
        private Dictionary<string, bool> users;
        
        public AuthenticationFacade()
        {
            users = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
        }

        public bool UserIsLoggedIn(string email)
        {
            if (!this.users.ContainsKey(email))
            {
                throw new MusicException("This email doesnt exist");
            }
            return this.users[email];
        }

        public void Login(string email)
        {
            if(email == null) 
                throw new ArgumentNullException("email");
            if (!this.users.ContainsKey(email))
            {
                throw new MusicException("This email doesnt exist");
            }
            if (this.users[email])
                throw new MusicException("This user is already logged in");
            users[email] = true;
        }

        public bool IsRegisterd(string email) 
        {
            if (!users.ContainsKey(email))
                throw new MusicException("this user doesnt exist");          
            return true;   
        }

        public bool Register(string email)
        {
            if (this.users.ContainsKey(email))
            {
                throw new MusicException("this user is already exist");
            }
            users.Add(email, true);
            return users[email];
        }

        public void Logout(string email) 
        {
            if(!this.users.ContainsKey(email))
            {
                throw new MusicException("This email doesnt exist");
            }
            if (!this.users[email])
                throw new MusicException("This user is already logged out");
            users[email] = false;
        }
        /// <summary>
        /// Adds a new user to the system without logging them in (typically used when loading data).
        /// </summary>
        /// <param name="Email">The email of the user to add.</param>
        /// <exception cref="KanbanException">Thrown if a user with the same email already exists.</exception>
        /// <precondition>The provided email must not already exist in the users dictionary.</precondition>
        /// <postcondition>The user is registered and marked as disconnected (false).</postcondition>
        public void AddUser(string Email)
        {
            if (users.ContainsKey(Email))
            {
                throw new MusicException("This user is already exist");
            }
            users.Add(Email, false);
        }
        /// <summary>
        /// Clears all users from the authentication facade dictionary.
        /// </summary>
        /// <precondition>There is no precondition.</precondition>
        /// <postcondition>The Users dictionary is completely emptied.</postcondition>
        internal void DeleteAllUser()
        {
            users.Clear();
        }
    }
}
