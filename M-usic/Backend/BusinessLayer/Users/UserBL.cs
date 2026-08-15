using Backend.BusinessLayer.Exceptions;
using Backend.DataAccesLayer.Dto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BusinessLayer.Users
{
    internal class UserBL
    {
        private string email { get; set; }
        private string password { get; set; }
        private UserDto userDto { get; }

        internal string Email
        {
            get => email;
            set
            {
                isValidEmail(value);
                email = value;
            }
        }

        internal string Password
        {
            get => password;
            set
            {
                isValidPass(value);
                password = value;
            }
        }
        internal UserDto UserDto
        {
            get => userDto;
        }
        public UserBL(string email, string password)
        {
            userDto = new UserDto(email, password);
            Email = email;
            Password = password;
            UserDto.Insert();
        }
        internal UserBL(UserDto userDto)
        {
            if(userDto == null)
            {
                throw new MusicException("UserDto can't be null");
            }
            this.userDto = userDto;
            this.email = userDto.Email;
            this.password = userDto.Password;
        }

        public bool LogIn(string pass)
        {
            if(pass == password)
            {
                return true;
            }
            else
            {
                throw new MusicException("Invalid Password");
            }
        }

     


        /// <summary>
        /// Validates that the provided password meets the required security criteria.
        /// </summary>
        /// <param name="pass">The password string to validate.</param>
        /// <exception cref="KanbanException">Thrown if the password is null, empty, outside the length bounds (6-20), or lacks a digit, uppercase letter, or lowercase letter.</exception>
        /// <precondition>There is no precondition.</precondition>
        /// <postcondition>Validates successfully if all password strength and length requirements are met.</postcondition>
        private void isValidPass(string pass)
        {
            if (string.IsNullOrEmpty(pass) || string.IsNullOrWhiteSpace(pass))
            {
                throw new MusicException("Password can't be null or empty");
            }
            if (pass.Length > 20 || pass.Length < 6)
            {
                throw new MusicException("inValid Password Length");
            }
            if (!pass.Any(char.IsDigit))
            {
                throw new MusicException("Password munst include atleast one digit");
            }
            if (!pass.Any(char.IsUpper))
            {
                throw new MusicException("Password munst include atleast one upperCase word");
            }
            if (!pass.Any(char.IsLower))
            {
                throw new MusicException("Password munst include atleast one lowercase word");
            }
        }

        /// <summary>
        /// Validates that the provided email address is correctly formatted and not null or empty.
        /// </summary>
        /// <param name="Email">The email string to validate.</param>
        /// <exception cref="KanbanException">Thrown if the email is null, empty, contains invalid character sequences (like ".@"), or fails the MailAddress format check.</exception>
        /// <precondition>There is no precondition.</precondition>
        /// <postcondition>Validates successfully if the email address is in a standard, recognizable format.</postcondition>
        private void isValidEmail(string Email)
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrWhiteSpace(Email))
            {
                throw new MusicException("Email can't be null");
            }
            if (Email.Contains(".@"))
            {
                throw new MusicException("Invalid email address");
            }
            try
            {
                var adrressEmail = new MailAddress(Email);
            }
            catch (Exception ex)
            {
                throw new MusicException(ex.Message);
            }
        }
    }
}
