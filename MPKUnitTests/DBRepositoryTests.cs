using Microsoft.EntityFrameworkCore;
using Musicians_Pocket_Knife.Models;
using Musicians_Pocket_Knife.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPKUnitTests
{
    internal class DBRepositoryTests
    {
        //private DBRepository _dbRepository;
        //private MpkdbContext _context;

        //[SetUp]
        //public void SetUp()
        //{
        //    var options = new DbContextOptionsBuilder<MpkdbContext>();
        //    options.UseInMemoryDatabase("database");
        //    _context = new MpkdbContext(options.Options);
        //    _dbRepository = new DBRepository(_context);
        //}

        //[Test]
        //public void AddSongToPlaylistTest()
        //{
        //    //Arrange
        //    APISong testSong = new APISong()
        //    {
        //        song = new Song()
        //        {
        //            id = "X69",
        //            artist = new Artist()
        //            {
        //                name = "JOHN CENA"
        //            }
        //        }
        //    };
        //    User user = new User()
        //    {
        //        FirstName = "JAZZZ",
        //        GoogleId = "111222"
        //    };
        //    Playlist list = new Playlist()
        //    {
        //        Id = 1,
        //        ListTitle = "ListyBoi",
        //        UserId = 1,
        //        User = user
        //    };
        //    _context.Playlists.Add(list);
        //    _context.SaveChanges();

        //    Dbsong song = new Dbsong()
        //    {
        //        PlaylistId = 1,
        //        Apiid = "X69"
        //    };
        //    _context.Dbsongs.Add(song);
        //    _context.SaveChanges();

        //    var playlists = _context.Playlists.ToList();

        //    //Act
        //    var result = _dbRepository.AddSongToPlaylist("111222", testSong, "ListyBoi");

        //    //Assert
        //    Assert.IsNull(result);


        //}
    }
}
