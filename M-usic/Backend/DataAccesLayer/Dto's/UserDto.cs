using Backend.DataAccesLayer.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DataAccesLayer.Dto_s
{
    internal class UserDto
    {
        internal const string UserEmail = "Email";
        internal const string UserPassword = "Password";
        private string _email;
        private string _password;
        private UserController _userController;
        private bool _isPersisted;

        /// <summary>
        /// Gets or sets a value indicating whether the user is currently persisted in the database.
        /// </summary>
        internal bool IsPersisted
        {
            get
            {
                return _isPersisted;
            }
            set
            {
                _isPersisted = value;
            }
        }
        internal string Email
        {
            get => _email;
            set
            {
                if (IsPersisted)
                    _userController.Update(Email,UserEmail, value);
                _email = value;
            }
        }
        internal string Password
        {
            get => _password;
            set
            {
                if (IsPersisted)
                    _userController.Update(Email, UserPassword, value);
                _password = value;
            }
        }

        public UserDto(string email, string password)
        {
            _email = email;
            _password = password;
            _userController = new UserController();
            _isPersisted = false;
        }

        internal void Insert()
        {
            _userController.Insert(this);
            _isPersisted = true;
        }
        internal void Delete()
        {
            _userController.Delete(this);
        }
    }
    }
