using Backend.BusinessLayer.Exceptions;
using Backend.BusinessLayer.Users;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Backend.ServiceLayer
{
    internal class UserService
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private UserFacade uf;

        internal UserService(UserFacade userFacade)
        {
            if (userFacade == null)
            {
                log.Fatal("UserFacade is null");
                throw new ArgumentNullException("UserFacade cant be null");
            }
            this.uf = userFacade;
            log.Info("UserService initialized successfully");
        }

        /// <summary>
        /// Allows the user login into his account.
        /// </summary>
        /// <param name="email">The email address of the user to login</param>
        /// <param name="password">The password of the user to login</param>
        /// <returns>Return to the user the UserSL if the user's info is correct, otherwise return error message</returns>
        /// <precondition> The email and password must not be null. </precondition>
        /// <postcondition> If successful, returns serialized UserSL; otherwise, returns an error response. </postcondition>
        public string login(string email, string password)
        {
            log.Info($"Login attempt for email: {email}");
            try
            {
                UserBL user = uf.Login(email, password);
                UserSL userSL = new UserSL(user);
                Response res = new Response(null, userSL);
                log.Info($"Login successful for email: {email}");
                return JsonSerializer.Serialize(res);
            }
            catch (MusicException mex)
            {
                log.Warn("Failed to login " + "Reason: " + mex.Message);
                return ErrResponse(mex.Message);
            }
            catch (Exception ex)
            {
                log.Error("Unexpected error during login: " + ex.Message);
                return ErrResponse(ex.Message);
            }
        }
        /// <summary>
        /// Allows the user to create a new account.
        /// </summary>
        /// <param name="email">The email address of the user to login</param> 
        /// <param name="password">The password of the user to login</param>
        /// <returns>Return to the user the UserSL if the register process was successfully completed, otherwise an error will occur</returns>
        /// <precondition> The user email must not be already registered. </precondition>
        /// <postcondition> A new user account is created. </postcondition>
        public string Register(string email, string password)
        {
            log.Info($"Register attempt for email: {email}");
            try
            {
                UserBL user = uf.Register(email, password);
                UserSL userSL = new UserSL(user);
                Response res = new Response(null, userSL);
                log.Info($"Register successful for email: {email}");
                return JsonSerializer.Serialize(res);
            }
            catch (MusicException mex)
            {
                log.Warn("Failed to register " + "Reason: " + mex.Message);
                return ErrResponse(mex.Message);
            }
            catch (Exception ex)
            {
                log.Error("Unexpected error during registration: " + ex.Message);
                return ErrResponse(ex.Message);
            }
        }
        /// <summary>
        /// Allows the user logout from his account.
        /// </summary>
        /// <param name="email">The email address of the user to logout</param>
        /// <returns>a boolean response, unless an error occurs</returns>
        /// <precondition> The user must be currently logged in. </precondition>
        /// <postcondition> The user is logged out, and a success status is returned. </postcondition>
        public string Logout(string email)
        {
            log.Info($"Logout attempt for email: {email}");
            try
            {
                bool flag = uf.Logout(email);
                Response res = new Response(null, flag);
                log.Info($"Logout successful for email: {email}");
                return JsonSerializer.Serialize(res);
            }
            catch (MusicException mex)
            {
                log.Warn("Failed to logout " + "Reason: " + mex.Message);
                return ErrResponse(mex.Message);
            }
            catch (Exception ex)
            {
                log.Error("Unexpected error during logout: " + ex.Message);
                return ErrResponse(ex.Message);
            }
        }
        private string ErrResponse(string errMessage)
        {
            Response errorR = new Response(errMessage, null);
            return JsonSerializer.Serialize(errorR);
        }
    }
}
