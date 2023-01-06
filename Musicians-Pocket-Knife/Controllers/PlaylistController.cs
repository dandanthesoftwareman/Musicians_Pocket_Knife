using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Musicians_Pocket_Knife.Models;
using Musicians_Pocket_Knife.Repositories;

namespace Musicians_Pocket_Knife.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        MpkdbContext context = new MpkdbContext();
        DBRepository repository = new DBRepository();

        [HttpPost("CreatePlaylist")]
        public Playlist CreatePlaylist(string title, string id)
        {
            User user = context.Users.FirstOrDefault(u => u.GoogleId == id);
            Playlist playlist = new Playlist()
            {
                ListTitle = title,
                UserId = user.Id
            };
            context.Add(playlist);
            context.SaveChanges();
            return playlist;
        }

        [HttpGet("GetUserPlaylists")]
        public List<Playlist> GetUserPlaylists(string id)
        {
            return repository.GetUserPlaylists(id);
        }

        [HttpGet("ViewPlaylistDetails")]
        public List<Song> ViewPlaylistDetails(string title, string id)
        {
            return repository.ViewPlaylistDetails(title, id);
        }

        [HttpPost("AddSongToPlaylist")]
        public Song AddSongToPlaylist(string id, string songID, string listTitle)
        {
            return repository.AddSongToPlaylist(id,songID,listTitle);
        }
    }
}
