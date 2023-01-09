using Musicians_Pocket_Knife.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

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
        public List<Dbsong> ViewPlaylistDetails(string title, string id)
        {
            User user = context.Users.FirstOrDefault(u => u.GoogleId == id);
            Playlist playlist = context.Playlists.FirstOrDefault(p => p.ListTitle == title && p.UserId == user.Id);
            return context.Dbsongs.Where(s => s.PlaylistId == playlist.Id).ToList();
        }
        public List<Playlist> GetUserPlaylists(string id)
        {
            User user = context.Users.FirstOrDefault(u => u.GoogleId == id);
            return context.Playlists.Where(u => u.UserId == user.Id).ToList();
        }

        //public APISong GetSongDetails(string id)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("https://api.getsongbpm.com/");
        //        //client.DefaultRequestHeaders.Accept.Clear();
        //        //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        string endpoint = "song/";
        //        string key = Secret.apiKey;
        //        string url = endpoint+$"?api_key={key}&id={id}";
        //        //GET Method
        //        HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var JSONstring = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        //            return JsonConvert.DeserializeObject<APISong>(JSONstring);
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}
        //public Dbsong AddSongToPlaylist(string id, string songID, string listTitle)
        //{
        //    User user = context.Users.FirstOrDefault(u => u.GoogleId == id);
        //    Playlist playlist = context.Playlists.FirstOrDefault(p => p.ListTitle == listTitle && p.UserId == user.Id);
        //    APISong song = GetSongDetails(songID);
        //    Dbsong dbSong = new Dbsong()
        //    {
        //        PlaylistId = playlist.Id,
        //        Title = song.song.title,
        //        Artist = song.song.artist.name,
        //        Tempo = song.song.tempo,
        //        TimeSignature = song.song.time_sig,
        //        OriginalKey = song.song.key_of,
        //        TransposedKey = song.song.key_of
        //    };
        //    context.Dbsongs.Add(dbSong);
        //    context.SaveChanges();
        //    return dbSong;
        //}
    }
}
