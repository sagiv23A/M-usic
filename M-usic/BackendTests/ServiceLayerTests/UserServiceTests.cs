using System;
using System.Text.Json;
using NUnit.Framework;
using Backend.ServiceLayer;
using Backend.BusinessLayer;
using Backend.BusinessLayer.Users;
using Backend.BusinessLayer.Exceptions;
//using Assert = NUnit.Framework.Legacy.ClassicAssert;
using Backend.BusinessLayer.Cross_Cutting; 

namespace BackendTests.ServiceLayerTests
{
    [TestFixture]
    public class UserServiceTest
    {
        private FacadeFactory _factory;
        private UserService us;

        [SetUp]
        public void Setup()
        {
            _factory = new FacadeFactory();
            us = _factory.User; // או דרך הבונה הייעודי במידה ואין FacadeFactory
        }

        [TearDown]
        public void TearDown()
        {
            // ניקוי נתונים לאחר כל טסט כדי לשמור על סביבה נקייה
            us.DeleteData();
            us = null;
            _factory = null;
        }

        #region Constructor Tests

        [Test]
        public void Constructor_UserService_WithValidFacade_CreatesSuccessfully()
        {
            AuthenticationFacade authFacade = new AuthenticationFacade();
            UserFacade facade = new UserFacade(authFacade);
            UserService service = new UserService(facade);

            Assert.IsNotNull(service);
        }

        [Test]
        public void Constructor_UserService_WithNullFacade_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new UserService(null));
        }

        #endregion

        #region Register Tests

        [TestCase("user@gmail.com", "Password123")]
        [TestCase("sagiv@gmail.com", "Cf234511")]
        [TestCase("tomi23@post.bgu.ac.il", "Ga334455")]
        public void Register_WithValidEmailAndPassword_ReturnsSuccessResponse(string email, string password)
        {
            string json = us.Register(email, password);
            Response response = JsonSerializer.Deserialize<Response>(json);

            Assert.IsNotNull(response);
            Assert.IsNull(response.ErrorMessage);
            Assert.IsNotNull(response.ReturnValue);
        }

        [TestCase("gmail.com", "Password123")]
        [TestCase("sagiv@", "Cf234511")]
        [TestCase("@gmail.com", "Ga334455")]
        [TestCase("plainText", "Ga334455")]
        public void Register_WithInvalidEmailFormat_ReturnsErrorResponse(string email, string password)
        {
            string json = us.Register(email, password);
            Response response = JsonSerializer.Deserialize<Response>(json);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.ErrorMessage);
        }

        [TestCase("", "Password123")]
        [TestCase("   ", "Password123")]
        [TestCase(null, "Password123")]
        public void Register_WithNullOrEmptyEmail_ReturnsErrorResponse(string email, string password)
        {
            string json = us.Register(email, password);
            Response response = JsonSerializer.Deserialize<Response>(json);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.ErrorMessage);
        }

        [TestCase("valid@gmail.com", "Pass1")]
        [TestCase("valid@gmail.com", "ThisIsAVeryLongPassword123456")]
        public void Register_PasswordLengthOutOfBounds_ReturnsErrorResponse(string email, string password)
        {
            string json = us.Register(email, password);
            Response response = JsonSerializer.Deserialize<Response>(json);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.ErrorMessage);
        }

        [TestCase("valid@gmail.com", "PASSWORD123")]
        [TestCase("valid@gmail.com", "password123")]
        [TestCase("valid@gmail.com", "PasswordWord")]
        [TestCase("valid@gmail.com", "")]
        [TestCase("valid@gmail.com", "   ")]
        [TestCase("valid@gmail.com", null)]
        public void Register_PasswordMissingRequiredCharactersOrEmpty_ReturnsErrorResponse(string email, string password)
        {
            string json = us.Register(email, password);
            Response response = JsonSerializer.Deserialize<Response>(json);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.ErrorMessage);
        }

        [Test]
        public void Register_SameUserTwice_ReturnsErrorResponse()
        {
            string email = "duplicate@gmail.com";
            string pass = "Password123";

            us.Register(email, pass);
            string json = us.Register(email, pass);
            Response response = JsonSerializer.Deserialize<Response>(json);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.ErrorMessage);
        }

        #endregion

        #region Login Tests

        [TestCase("login1@gmail.com", "Password123")]
        [TestCase("login2@gmail.com", "Cf234511")]
        public void Login_WithValidCredentials_ReturnsSuccessResponse(string email, string password)
        {
            us.Register(email, password);
            us.Logout(email);
            string json = us.login(email, password);
            Response response = JsonSerializer.Deserialize<Response>(json);

            Assert.IsNotNull(response);
            Assert.IsNull(response.ErrorMessage);
            Assert.IsNotNull(response.ReturnValue);
        }

        [Test]
        public void Login_WithWrongPassword_ReturnsErrorResponse()
        {
            string email = "wrongpass@gmail.com";
            us.Register(email, "Password123");

            string json = us.login(email, "WrongPass123");
            Response response = JsonSerializer.Deserialize<Response>(json);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.ErrorMessage);
        }

        [Test]
        public void Login_WithNonExistentUser_ReturnsErrorResponse()
        {
            string json = us.login("ghost@gmail.com", "Password123");
            Response response = JsonSerializer.Deserialize<Response>(json);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.ErrorMessage);
        }

        [TestCase("", "Password123")]
        [TestCase(null, "Password123")]
        public void Login_NullOrEmptyEmail_ReturnsErrorResponse(string email, string password)
        {
            string json = us.login(email, password);
            Response response = JsonSerializer.Deserialize<Response>(json);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.ErrorMessage);
        }

        [Test]
        public void Login_PasswordWithNullValue_ReturnsErrorResponse()
        {
            string email = "safetynet@gmail.com";
            us.Register(email, "Password123");

            string json = us.login(email, null);
            Response response = JsonSerializer.Deserialize<Response>(json);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.ErrorMessage);
        }

        [Test]
        public void Login_UserAlreadyLoggedIn_ReturnsErrorResponse()
        {
            string email = "doublelogin@gmail.com";
            string pass = "Password123";

            us.Register(email, pass); // הרשמה מבצעת התחברות אוטומטית במערכת
            string json = us.login(email, pass); // ניסיון התחברות נוסף
            Response response = JsonSerializer.Deserialize<Response>(json);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.ErrorMessage);
        }

        [Test]
        public void Login_AfterLogout_ReturnsSuccessResponse()
        {
            string email = "relogin@gmail.com";
            string pass = "Password123";

            us.Register(email, pass);
            us.Logout(email);

            string json = us.login(email, pass);
            Response response = JsonSerializer.Deserialize<Response>(json);

            Assert.IsNotNull(response);
            Assert.IsNull(response.ErrorMessage);
        }

        #endregion

        #region Logout Tests

        [Test]
        public void LogOut_WithRegisteredAndLoggedInUser_ReturnsSuccessResponse()
        {
            string email = "logout@gmail.com";
            string pass = "Password123";

            us.Register(email, pass);
            us.login(email, pass);

            string json = us.Logout(email);
            Response response = JsonSerializer.Deserialize<Response>(json);

            Assert.IsNotNull(response);
            Assert.IsNull(response.ErrorMessage);
        }

        [Test]
        public void LogOut_UserNotLoggedIn_ReturnsErrorResponse()
        {
            string email = "notlogged@gmail.com";
            us.Register(email, "Password123");
            us.Logout(email); // כעת המשתמש מנותק

            string json = us.Logout(email); // ניסיון להתנתק שוב
            Response response = JsonSerializer.Deserialize<Response>(json);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.ErrorMessage);
        }

        [Test]
        public void LogOut_NonExistentUser_ReturnsErrorResponse()
        {
            string json = us.Logout("neverexisted@gmail.com");
            Response response = JsonSerializer.Deserialize<Response>(json);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.ErrorMessage);
        }

        [TestCase("")]
        [TestCase(null)]
        public void LogOut_NullOrEmptyEmail_ReturnsErrorResponse(string email)
        {
            string json = us.Logout(email);
            Response response = JsonSerializer.Deserialize<Response>(json);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.ErrorMessage);
        }

        [Test]
        public void LogOut_CalledTwice_SecondCallReturnsErrorResponse()
        {
            string email = "doublelogout@gmail.com";
            string pass = "Password123";

            us.Register(email, pass);
            us.Logout(email);

            string json = us.Logout(email); // קריאה שנייה
            Response response = JsonSerializer.Deserialize<Response>(json);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.ErrorMessage);
        }

        #endregion
    }
}