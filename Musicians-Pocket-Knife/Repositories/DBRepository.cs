using Microsoft.EntityFrameworkCore;
using Musicians_Pocket_Knife.Models;
using Newtonsoft.Json;

namespace Musicians_Pocket_Knife.Repositories
{
    public class DBRepository
    {
        MpkdbContext context = new MpkdbContext();
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
        public List<Playlist> GetUserPlaylists(string id)
        {
            User user = context.Users.FirstOrDefault(u => u.GoogleId == id);
            return context.Playlists.Where(u => u.UserId == user.Id).ToList();
        }
        public List<Dbsong> ViewPlaylistDetails(string title, string id)
        {
            User user = context.Users.FirstOrDefault(u => u.GoogleId == id);
            Playlist playlist = context.Playlists.FirstOrDefault(p => p.ListTitle == title && p.UserId == user.Id);
            return context.Dbsongs.Where(s => s.PlaylistId == playlist.Id).ToList();
        }
        public Dbsong AddSongToPlaylist(string id, string songID, string listTitle)
        {
            User user = context.Users.FirstOrDefault(u => u.GoogleId == id);
            Playlist playlist = context.Playlists.FirstOrDefault(p => p.ListTitle == listTitle && p.UserId == user.Id);
            APISong song = GetSongDetails(songID);
            Dbsong dbSong = new Dbsong()
            {
                PlaylistId = playlist.Id,
                Title = song.song.title,
                Apiid = song.song.id,
                Artist = song.song.artist.name,
                Tempo = song.song.tempo,
                TimeSignature = song.song.time_sig,
                OriginalKey = song.song.key_of,
                TransposedKey = song.song.key_of
            };
            context.Dbsongs.Add(dbSong);
            context.SaveChanges();
            return dbSong;
        }
        public APISong GetSongDetails(string SongId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.getsongbpm.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");


                string endpoint = "song/";
                string key = Secret.apiKey;
                string url = endpoint+$"?api_key={key}&id={SongId}";
                //GET Method
                HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    var JSONstring = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    return JsonConvert.DeserializeObject<APISong>(JSONstring);
                }
                else
                {
                    return null;
                }
            }
        }
        public Dbsong GetDBSongDetails(string id, string songID, string listTitle)
        {
            User user = context.Users.FirstOrDefault(u => u.GoogleId == id);
            Playlist playlist = context.Playlists.FirstOrDefault(p => p.ListTitle == listTitle && p.UserId == user.Id);
            return context.Dbsongs.FirstOrDefault(s => s.Apiid == songID);
        }
    }
}
