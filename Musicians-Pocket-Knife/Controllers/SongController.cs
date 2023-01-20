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

        [HttpGet("GetDBSongDetails")]
        public Dbsong GetDBSongDetails(string id, string songID, string listTitle)
        {
            return repository.GetDBSongDetails(id, songID, listTitle);
        }
        [HttpPatch("SaveTransposeChanges")]
        public void SaveTransposeChanges([FromBody]List<Dbsong> songs, string listTitle, string id)
        {
            repository.SaveTransposeChanges(songs, listTitle, id);
        }
    }
}
