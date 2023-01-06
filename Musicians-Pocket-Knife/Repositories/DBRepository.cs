using Musicians_Pocket_Knife.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Musicians_Pocket_Knife.Repositories
{
    public class DBRepository
    {
        MpkdbContext context = new MpkdbContext();
        public List<Song> ViewPlaylistDetails(string title, string id)
        {
            User user = context.Users.FirstOrDefault(u => u.GoogleId == id);
            Playlist playlist = context.Playlists.FirstOrDefault(p => p.ListTitle == title && p.UserId == user.Id);
            return context.Songs.Where(s => s.PlaylistId == playlist.Id).ToList();
        }
        public List<Playlist> GetUserPlaylists(string id)
        {
            User user = context.Users.FirstOrDefault(u => u.GoogleId == id);
            return context.Playlists.Where(u => u.UserId == user.Id).ToList();
        }

        public Song GetSongDetails(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.getsongbpm.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string endpoint = "song/";
                string key = Secret.apiKey;
                string url = endpoint+$"?api_key={key}&id={id}";
                //GET Method
                HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    var JSONstring = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    return JsonConvert.DeserializeObject<Song>(JSONstring);
                }
                else
                {
                    return null;
                }
            }
        }
        public Song AddSongToPlaylist(string id, string songID, string listTitle)
        {
            User user = context.Users.FirstOrDefault(u => u.GoogleId == id);
            Playlist playlist = context.Playlists.FirstOrDefault(p => p.ListTitle == listTitle && p.UserId == user.Id);
            Song song = GetSongDetails(songID);
            song.TransposedKey = song.OriginalKey;
            song.PlaylistId = playlist.Id;
            context.Songs.Add(song);
            context.SaveChanges();
            return song;
        }
    }
}
