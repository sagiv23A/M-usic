using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;
using Backend.BusinessLayer.Song;
using Backend.BusinessLayer.Cross_Cutting;
using Backend.BusinessLayer.Users;
//using Assert = NUnit.Framework.Legacy.ClassicAssert;

namespace BackendTests.BusinessLayerTests
{
    [TestFixture]
    public class SongFacadeTests
    {
        private ISpotifyRepository _fakeSpotifyRepo;
        private IYoutubeRepository _fakeYoutubeRepo;
        private SongFacade _songFacade;

        [SetUp]
        public void Setup()
        {
            _fakeSpotifyRepo = new FakeSpotifyRepository();
            _fakeYoutubeRepo = new FakeYoutubeRepository();

            _songFacade = new SongFacade(_fakeSpotifyRepo, _fakeYoutubeRepo);
        }

        [TearDown]
        public void TearDown()
        {
            _songFacade = null;
            _fakeSpotifyRepo = null;
            _fakeYoutubeRepo = null;
        }

        #region Constructor Tests

        [Test]
        public void Constructor_ValidRepositories_CreatesInstanceSuccessfully()
        {
            Assert.IsNotNull(_songFacade);
        }

        [Test]
        public void Constructor_NullSpotifyRepository_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new SongFacade(null, _fakeYoutubeRepo));
        }

        [Test]
        public void Constructor_NullYoutubeRepository_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new SongFacade(_fakeSpotifyRepo, null));
        }

        #endregion

        #region SearchSongs Tests

        [Test]
        public async Task SearchSongs_ValidQuery_ReturnsSongList()
        {
            // Act
            IEnumerable<SongBL> result = await _songFacade.SearchSongs("Queen");

            // Assert
            Assert.IsNotNull(result);
        }

        [TestCase("")]
        [TestCase("   ")]
        [TestCase(null)]
        public void SearchSongs_NullOrEmptyQuery_ThrowsArgumentException(string invalidQuery)
        {
            Assert.ThrowsAsync<ArgumentException>(async () => await _songFacade.SearchSongs(invalidQuery));
        }

        #endregion

        #region GetAudioStream Tests

        [Test]
        public async Task GetAudioStream_ValidSongId_ReturnsStream()
        {
            // Act
            Stream result = await _songFacade.GetAudioStream("song123");

            // Assert
            Assert.IsNotNull(result);
        }

        [TestCase("")]
        [TestCase("   ")]
        [TestCase(null)]
        public void GetAudioStream_NullOrEmptySongId_ThrowsArgumentException(string invalidSongId)
        {
            Assert.ThrowsAsync<ArgumentException>(async () => await _songFacade.GetAudioStream(invalidSongId));
        }

        #endregion

        #region Database & Cache Tests

        [Test]
        public void SelectAllSongs_ExecutesWithoutExceptions()
        {
            Assert.DoesNotThrow(() => _songFacade.SelectAllSongs());
        }

        [Test]
        public void DeleteAllSongs_ExecutesWithoutExceptions()
        {
            Assert.DoesNotThrow(() => _songFacade.DeleteAllSongs());
        }

        #endregion
    }

    #region Fake Classes For Testing (Without Moq)

    internal class FakeSpotifyRepository : ISpotifyRepository
    {
        public Task<IEnumerable<SongBL>> GetSongInfoAsync(string query)
        {
            var songs = new List<SongBL>
            {
                new SongBL("Bohemian Rhapsody", "05:55", new List<string> { "Queen" }, "song123", "http://cover.jpg")
            };
            return Task.FromResult<IEnumerable<SongBL>>(songs);
        }
    }

    internal class FakeYoutubeRepository : IYoutubeRepository
    {
        public Task<Stream> GetAudioStreamAsync(string songId)
        {
            Stream dummyStream = new MemoryStream();
            return Task.FromResult(dummyStream);
        }
    }

    #endregion
}