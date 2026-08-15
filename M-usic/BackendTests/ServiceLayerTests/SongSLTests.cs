using System;
using System.Collections.Generic;
using System.Text.Json;
using NUnit.Framework;
using Backend.ServiceLayer;
using Backend.BusinessLayer.Song;
//using Assert = NUnit.Framework.Legacy.ClassicAssert;

namespace BackendTests.ServiceLayerTests
{
    [TestFixture]
    public class SongSLTests
    {
        #region Constructor Tests

        [Test]
        public void Constructor_Parameterized_InitializesPropertiesCorrectly()
        {
            // Arrange
            string title = "Bohemian Rhapsody";
            string duration = "05:55";
            List<string> artists = new List<string> { "Queen" };
            string songId = "song123";
            string coverArtURL = "https://example.com/cover.jpg";

            // Act
            SongSL songSL = new SongSL(title, duration, artists, songId, coverArtURL);

            // Assert
            Assert.AreEqual(title, songSL.title);
            Assert.AreEqual(duration, songSL.duration);
            Assert.AreEqual(artists, songSL.artists);
            Assert.AreEqual(songId, songSL.songId);
            Assert.AreEqual(coverArtURL, songSL.coverArtURL);
        }

        [Test]
        public void Constructor_Parameterless_CreatesInstanceWithNullProperties()
        {
            // Act
            SongSL songSL = new SongSL();

            // Assert
            Assert.IsNotNull(songSL);
            Assert.IsNull(songSL.title);
            Assert.IsNull(songSL.duration);
            Assert.IsNull(songSL.artists);
            Assert.IsNull(songSL.songId);
            Assert.IsNull(songSL.coverArtURL);
        }

        [Test]
        public void Constructor_FromSongBL_CopiesAllPropertiesCorrectly()
        {
            // Arrange
            string title = "Hotel California";
            string duration = "06:30";
            List<string> artists = new List<string> { "Eagles" };
            string songId = "song456";
            string coverArtURL = "https://example.com/eagles.jpg";

            // יצירת אובייקט SongBL (בהתאם לבנאי או לשדות של SongBL אצלכם)
            SongBL songBL = new SongBL(title, duration, artists, songId, coverArtURL);

            // Act
            SongSL songSL = new SongSL(songBL);

            // Assert
            Assert.AreEqual(songBL.title, songSL.title);
            Assert.AreEqual(songBL.duration, songSL.duration);
            Assert.AreEqual(songBL.artists, songSL.artists);
            Assert.AreEqual(songBL.songId, songSL.songId);
            Assert.AreEqual(songBL.coverArtURL, songSL.coverArtURL);
        }

        #endregion

        #region Serialization Tests

        [Test]
        public void SongSL_SerializationAndDeserialization_MaintainsDataIntegrity()
        {
            // Arrange
            SongSL originalSong = new SongSL(
                "Imagine",
                "03:04",
                new List<string> { "John Lennon" },
                "song789",
                "https://example.com/imagine.jpg"
            );

            // Act
            string json = JsonSerializer.Serialize(originalSong);
            SongSL deserializedSong = JsonSerializer.Deserialize<SongSL>(json);

            // Assert
            Assert.IsNotNull(deserializedSong);
            Assert.AreEqual(originalSong.title, deserializedSong.title);
            Assert.AreEqual(originalSong.duration, deserializedSong.duration);
            Assert.AreEqual(originalSong.artists, deserializedSong.artists);
            Assert.AreEqual(originalSong.songId, deserializedSong.songId);
            Assert.AreEqual(originalSong.coverArtURL, deserializedSong.coverArtURL);
        }

        #endregion
    }
}