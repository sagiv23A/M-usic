using NUnit.Framework;
using Backend.ServiceLayer;
using Backend.BusinessLayer.Users;
using System;

namespace Backend.Tests
{
    [TestFixture]
    public class UserSLTests
    {
        [Test]
        public void Constructor_WithEmailAndUserId_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            string expectedEmail = "user@example.com";
            string expectedUserId = "user_123";

            // Act
            UserSL user = new UserSL(expectedEmail, expectedUserId);

            // Assert - התחביר המעודכן ב-NUnit 4
            Assert.That(user.email, Is.EqualTo(expectedEmail), "Email property was not set correctly");
            Assert.That(user.userId, Is.EqualTo(expectedUserId), "UserId property was not set correctly");
        }

        [Test]
        public void DefaultConstructor_ShouldInitializeWithNullProperties()
        {
            // Act
            UserSL user = new UserSL();

            // Assert
            Assert.That(user.email, Is.Null, "Default email should be null");
            Assert.That(user.userId, Is.Null, "Default userId should be null");
        }

        [Test]
        public void Constructor_WithValidUserBL_ShouldSetEmail()
        {
            // Arrange
            UserBL userBL = new UserBL("user@example.com", "Password123");
            ;

            // Act
            UserSL user = new UserSL(userBL);

            // Assert
            Assert.That(user.email, Is.EqualTo("user@example.com"));
        }

        [Test]
        public void Constructor_WithNullUserBL_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new UserSL((UserBL)null));
        }
    }
}