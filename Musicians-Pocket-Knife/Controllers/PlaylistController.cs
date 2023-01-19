﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public Playlist CreatePlaylist(string listTitle, string id)
        {
            return repository.CreatePlaylist(listTitle, id);
        }
        [HttpDelete("DeletePlaylist")]
        public Playlist DeletePlaylist(string listTitle, string id)
        {
            return repository.DeletePlaylist(listTitle, id);
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
        public Dbsong AddSongToPlaylist([FromBody]APISong song, string id, string listTitle)
        {
            try
            {
                return repository.AddSongToPlaylist(id, song, listTitle);
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
            return repository.RemoveSongFromPlaylist(id, songID, listTitle);
        }
    }
}
