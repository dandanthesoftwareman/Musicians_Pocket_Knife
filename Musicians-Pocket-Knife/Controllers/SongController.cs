using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Musicians_Pocket_Knife.Models;
using Musicians_Pocket_Knife.Repositories;

namespace Musicians_Pocket_Knife.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        DBRepository repository = new DBRepository();

        [HttpGet("GetSongDetails")]
        public APISong GetSongDetails(string SongID)
        {
            return repository.GetSongDetails(SongID);
        }

        [HttpGet("GetDBSongDetails")]
        public Dbsong GetDBSongDetails(string id, string songID, string listTitle)
        {
            return repository.GetDBSongDetails(id, songID, listTitle);
        }
    }
}
