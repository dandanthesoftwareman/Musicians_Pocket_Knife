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
        private readonly IPlaylistRepository playlistRepository;
        private readonly ISongRepository songRepository;

        public PlaylistController(
            IPlaylistRepository playlistRepository,
            ISongRepository songRepository)
        {
            this.playlistRepository = playlistRepository;
            this.songRepository = songRepository;
        }

        [HttpPost("CreatePlaylist")]
        public Playlist CreatePlaylist(string listTitle, string id)
        {
            return playlistRepository.CreatePlaylist(listTitle, id);
        }

        [HttpPatch("RenamePlaylist")]
        public Playlist RenamePlaylist(int listId, string newTitle, string id)
        {
            return playlistRepository.RenamePlaylist(listId, newTitle, id);
        }

        [HttpDelete("DeletePlaylist")]
        public void DeletePlaylist(int listId, string id)
        {
            playlistRepository.DeletePlaylist(listId, id);
        }

        [HttpGet("GetListTitle")]
        public Playlist GetListTitle(int listId, string id)
        {
            return playlistRepository.GetListTitle(listId, id);
        }

        [HttpGet("GetUserPlaylists")]
        public List<Playlist> GetUserPlaylists(string id)
        {
            return playlistRepository.GetUserPlaylists(id);
        }

        [HttpGet("ViewPlaylistDetails")]
        public List<Dbsong> ViewPlaylistDetails(int listId, string id)
        {
            return playlistRepository.ViewPlaylistDetails(listId, id);
        }

        [HttpPost("AddSongToPlaylist")]
        public Dbsong AddSongToPlaylist([FromBody]APISong song, string id, int listId)
        {
            try
            {
                return songRepository.AddSongToPlaylist(id, song, listId);
            }
            catch (Exception ex)
            {
                return new Dbsong
                {
                    Title = ex.Message,
                };
            }
        }

        [HttpDelete("RemoveSongFromPlaylist")]
        public Dbsong RemoveSongFromPlaylist(string id, int songID, int listId)
        {
            return songRepository.RemoveSongFromPlaylist(id, songID, listId);
        }

        [HttpPatch("UpdateDateViewed")]
        public void UpdateDateViewed(int listId, string id)
        {
            playlistRepository.UpdateDateViewed(listId, id);
        }

        [HttpPatch("UpdateSongIndexes")]
        public void UpdateSongIndexes([FromBody]List<Dbsong> songs)
        {
            songRepository.UpdateSongIndexes(songs);
        }
    }
}
