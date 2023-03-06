using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Musicians_Pocket_Knife.Models;
using Musicians_Pocket_Knife.Repositories;

namespace Musicians_Pocket_Knife.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        public PlaylistController(IDBRepository repository)
        {
            _repository = repository;
        }
        IDBRepository _repository;

        [HttpPost("CreatePlaylist")]
        public Playlist CreatePlaylist(string listTitle, string id)
        {
            return _repository.CreatePlaylist(listTitle, id);
        }
        [HttpPatch("RenamePlaylist")]
        public Playlist RenamePlaylist(int listId, string newTitle, string id)
        {
            return _repository.RenamePlaylist(listId, newTitle, id);
        }
        [HttpDelete("DeletePlaylist")]
        public void DeletePlaylist(int listId, string id)
        {
            _repository.DeletePlaylist(listId, id);
        }
        [HttpGet("GetListTitle")]
        public Playlist GetListTitle(int listId, string id)
        {
            return _repository.GetListTitle(listId, id);
        }
        [HttpGet("GetUserPlaylists")]
        public List<Playlist> GetUserPlaylists(string id)
        {
            return _repository.GetUserPlaylists(id);
        }

        [HttpGet("ViewPlaylistDetails")]
        public List<Dbsong> ViewPlaylistDetails(int listId, string id)
        {
            return _repository.ViewPlaylistDetails(listId, id);
        }

        [HttpPost("AddSongToPlaylist")]
        public Dbsong AddSongToPlaylist([FromBody]APISong song, string id, int listId)
        {
            try
            {
                return _repository.AddSongToPlaylist(id, song, listId);
            }
            catch(Exception ex)
            {
                return new Dbsong
                {
                    Title = ex.Message,
                };
            }
            
        }
        [HttpDelete("RemoveSongFromPlaylist")]
        public Dbsong RemoveSongFromPlaylist(string id, int songID, string listTitle)
        {
            return _repository.RemoveSongFromPlaylist(id, songID, listTitle);
        }
        [HttpPatch("UpdateDateViewed")]
        public void UpdateDateViewed(int listId, string id)
        {
            _repository.UpdateDateViewed(listId, id);
        }
    }
}
