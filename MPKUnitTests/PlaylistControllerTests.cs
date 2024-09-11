using Musicians_Pocket_Knife.Controllers;
using Musicians_Pocket_Knife.Models;
using Musicians_Pocket_Knife.Repositories;
using NSubstitute;
using NUnit.Framework;

namespace MPKUnitTests
{
    //public class PlaylistControllerTests 
    //{
    //    PlaylistController _controller;
    //    IPlaylistRepository _repository;
    //    [SetUp]
    //    public void Setup()
    //    {
    //        _repository = Substitute.For<IPlaylistRepository>();
    //        _controller = new PlaylistController(_repository);
    //    }

    //    [Test]
    //    public void Test1()
    //    {
    //        string id = "Jazz Man";
    //        APISong song = new APISong();
    //        int listId = 1;
    //        Dbsong expected = new Dbsong()
    //        {
    //            Id = 5,
    //            Title = "Jazzy Jazz"
    //        };

    //        _repository.AddSongToPlaylist(id, song, listId).Returns(expected);
    //        var result = _controller.AddSongToPlaylist(song, id, listId);
    //        Assert.AreEqual(expected, result);
    //    }
    //}
}