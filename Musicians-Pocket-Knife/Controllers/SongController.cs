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
        private readonly ISongRepository songRepository;

        public SongController(ISongRepository songRepository)
        {
            this.songRepository = songRepository;
        }
        
        [HttpGet("GetDBSongDetails")]
        public Dbsong GetDBSongDetails(string id, string songID, string listTitle)
        {
            return songRepository.GetDBSongDetails(id, songID, listTitle);
        }

        [HttpPatch("SaveTransposeChanges")]
        public async Task SaveTransposeChanges([FromBody]List<Dbsong> songs)
        {
            await songRepository.SaveTransposeChanges(songs);
        }
    }
}
