using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Musicians_Pocket_Knife.Models;
using Musicians_Pocket_Knife.Orchestrators;
using Musicians_Pocket_Knife.Repositories;

namespace Musicians_Pocket_Knife.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly IPlaylistRepository _playlistRepository;
        private readonly IPlaylistOrchestrator _playlistOrchestrator;
        private readonly ISongRepository _songRepository;

        public PlaylistController(
            IPlaylistOrchestrator playlistOrchestrator,
            IPlaylistRepository playlistRepository,
            ISongRepository songRepository)
        {
            _playlistOrchestrator = playlistOrchestrator;
            _playlistRepository = playlistRepository;
            _songRepository = songRepository;
        }

        [HttpPost("CreatePlaylist")]
        public async Task<IActionResult> CreateNewPlaylist(CreateNewPlaylistRequest request)
        {
            var response = await _playlistOrchestrator.CreateNewPlaylist(request);
            return Ok(response);
        }

        [HttpPatch("RenamePlaylist")]
        public Playlist RenamePlaylist(int listId, string newTitle, string id)
        {
            return _playlistRepository.RenamePlaylist(listId, newTitle, id);
        }

        [HttpDelete("DeletePlaylist")]
        public void DeletePlaylist(int listId, string id)
        {
            _playlistRepository.DeletePlaylist(listId, id);
        }

        [HttpGet("GetListTitle")]
        public Playlist GetListTitle(int listId, string id)
        {
            return _playlistRepository.GetListTitle(listId, id);
        }

        [HttpGet("GetUserPlaylists")]
        public List<Playlist> GetUserPlaylists(string id)
        {
            return _playlistRepository.GetUserPlaylists(id);
        }

        [HttpGet("ViewPlaylistDetails")]
        public List<Dbsong> ViewPlaylistDetails(int listId, string id)
        {
            return _playlistRepository.ViewPlaylistDetails(listId, id);
        }

        [HttpPost("AddSongToPlaylist")]
        public Dbsong AddSongToPlaylist([FromBody]APISong song, string id, int listId)
        {
            try
            {
                return _songRepository.AddSongToPlaylist(id, song, listId);
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
            return _songRepository.RemoveSongFromPlaylist(id, songID, listId);
        }

        [HttpPatch("UpdateDateViewed")]
        public void UpdateDateViewed(int listId, string id)
        {
            _playlistRepository.UpdateDateViewed(listId, id);
        }

        [HttpPatch("UpdateSongIndexes")]
        public void UpdateSongIndexes([FromBody]List<Dbsong> songs)
        {
            _songRepository.UpdateSongIndexes(songs);
        }
    }
}
