using NUnit.Framework;
using Backend.ServiceLayer;
//using Assert = NUnit.Framework.Legacy.ClassicAssert;

namespace BackendTests.ServiceLayerTests
{
    [TestFixture]
    public class FacadeFactoryTests
    {
        private FacadeFactory _factory;

        [SetUp]
        public void Setup()
        {
            _factory = new FacadeFactory();
        }

        [TearDown]
        public void TearDown()
        {
            _factory = null;
        }

        #region Initialization Tests

        [Test]
        public void Constructor_InitializesFactoryAndServicesSuccessfully()
        {
            Assert.IsNotNull(_factory);
            Assert.IsNotNull(_factory.User);
            Assert.IsNotNull(_factory.Song);
        }

        [Test]
        public void Properties_ReturnSameServiceInstances()
        {
            UserService firstUserCall = _factory.User;
            UserService secondUserCall = _factory.User;

            SongService firstSongCall = _factory.Song;
            SongService secondSongCall = _factory.Song;

            Assert.AreSame(firstUserCall, secondUserCall);
            Assert.AreSame(firstSongCall, secondSongCall);
        }

        #endregion
    }
}