﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Musicians_Pocket_Knife.Models;

namespace Musicians_Pocket_Knife.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistContollercs : ControllerBase
    {
        MpkdbContext context = new MpkdbContext();

        [HttpPost("CreatePlaylist")]
        public void CreatePlaylist(string title, string id)
        {
            User user = context.Users.FirstOrDefault(u => u.GoogleId == id);
            Playlist playlist = new Playlist()
            {
                ListTitle = title,
                UserId = user.Id
            };
            context.Add(playlist);
            context.SaveChanges();
        }
    }
}
