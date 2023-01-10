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
        DBRepository repository = new DBRepository();

        [HttpPost("CreatePlaylist")]
        public Playlist CreatePlaylist(string title, string id)
        {
            return repository.CreatePlaylist(title, id);
        }

        [HttpGet("GetUserPlaylists")]
        public List<Playlist> GetUserPlaylists(string id)
        {
            return repository.GetUserPlaylists(id);
        }

        [HttpGet("ViewPlaylistDetails")]
        public List<Dbsong> ViewPlaylistDetails(string title, string id)
        {
            return repository.ViewPlaylistDetails(title, id);
        }

        [HttpPost("AddSongToPlaylist")]
        public Dbsong addsongtoplaylist(string id, string songid, string listtitle)
        {
            return repository.AddSongToPlaylist(id, songid, listtitle);
        }
    }
}
