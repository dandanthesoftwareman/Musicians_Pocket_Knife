using Musicians_Pocket_Knife.Controllers;
using Musicians_Pocket_Knife.Models;
using Musicians_Pocket_Knife.Repositories;
using NSubstitute;
using NUnit.Framework;

namespace MPKUnitTests
{
    public class PlaylistControllerTests 
    {
        PlaylistController _controller;
        IDBRepository _repository;
        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IDBRepository>();
            _controller = new PlaylistController(_repository);
        }

        [Test]
        public void Test1()
        {
            string id = "Jazz Man";
            APISong song = new APISong();
            string listTitle = "Jazz Man's Jams";
            Dbsong expected = new Dbsong()
            {
                Id = 5,
                Title = "Jazzy Jazz"
            };

            _repository.AddSongToPlaylist(id, song, listTitle).Returns(expected);
            var result = _controller.AddSongToPlaylist(song, id, listTitle);
            Assert.AreEqual(expected, result);
        }
    }
}