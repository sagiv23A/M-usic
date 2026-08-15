using System;
using System.IO;
using NUnit.Framework;
using Backend.ServiceLayer;
//using Assert = NUnit.Framework.Legacy.ClassicAssert;

namespace BackendTests.ServiceLayerTests
{
    [TestFixture]
    public class AudioStreamResultTests
    {
        #region Constructor Tests

        [Test]
        public void Constructor_ThreeParameters_ValidArguments_InitializesPropertiesCorrectly()
        {
            // Arrange
            using Stream fakeStream = new MemoryStream();
            string contentType = "audio/mpeg";
            long contentLength = 2048;

            // Act
            AudioStreamResult result = new AudioStreamResult(fakeStream, contentType, contentLength);

            // Assert
            Assert.AreEqual(fakeStream, result.streamContent);
            Assert.AreEqual(contentType, result.contentType);
            Assert.AreEqual(contentLength, result.contentLength);
        }

        [Test]
        public void Constructor_TwoParameters_ValidArguments_DefaultsContentLengthToZero()
        {
            // Arrange
            using Stream fakeStream = new MemoryStream();
            string contentType = "audio/wav";

            // Act
            AudioStreamResult result = new AudioStreamResult(fakeStream, contentType);

            // Assert
            Assert.AreEqual(fakeStream, result.streamContent);
            Assert.AreEqual(contentType, result.contentType);
            Assert.AreEqual(0, result.contentLength);
        }

        [Test]
        public void Constructor_NullStream_ThrowsArgumentNullException()
        {
            // Arrange
            string contentType = "audio/mpeg";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AudioStreamResult(null, contentType));
            Assert.Throws<ArgumentNullException>(() => new AudioStreamResult(null, contentType, 500));
        }

        #endregion
    }
}