﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Musicians_Pocket_Knife.Models;

namespace Musicians_Pocket_Knife.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        MpkdbContext context = new MpkdbContext();

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
            User user = context.Users.FirstOrDefault(u => u.GoogleId == id);
            return context.Playlists.Include(u => u.User).Where(u => u.UserId == user.Id).ToList();
        }
    }
}
