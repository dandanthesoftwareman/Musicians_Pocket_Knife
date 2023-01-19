using Microsoft.EntityFrameworkCore;
using Musicians_Pocket_Knife.Models;
using Newtonsoft.Json;

namespace Musicians_Pocket_Knife.Repositories
{
    public class DBRepository
    {
        MpkdbContext context = new MpkdbContext();

        //PLAYLIST Methods
        public Playlist CreatePlaylist(string listTitle, string id)
        {
            User user = context.Users.FirstOrDefault(u => u.GoogleId == id);
            Playlist playlist = new Playlist()
            {
                ListTitle = listTitle,
                UserId = user.Id
            };
            if(context.Playlists.Any(x => x.ListTitle == listTitle))
            {
                return null;
            }
            else
            {
                context.Add(playlist);
                context.SaveChanges();
                return playlist;
            }
            
        }
        public Playlist DeletePlaylist(string listTitle, string id)
        {
            User user = context.Users.FirstOrDefault(u => u.GoogleId == id);
            Playlist playlist = context.Playlists.FirstOrDefault(x => x.ListTitle == listTitle && x.UserId == user.Id);
            List<Dbsong> songs = context.Dbsongs.Where(x => x.PlaylistId == playlist.Id).ToList();
            if(songs != null)
            {
                foreach (Dbsong s in songs)
                {
                    context.Remove(s);
                }
            }
            context.Remove(playlist);
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
        public Dbsong AddSongToPlaylist(string id, APISong song, string listTitle)
        {
            User user = context.Users.FirstOrDefault(u => u.GoogleId == id);
            Playlist playlist = context.Playlists.FirstOrDefault(p => p.ListTitle == listTitle && p.UserId == user.Id);
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
            if(context.Dbsongs.Any(x => x.Apiid == song.song.id && x.PlaylistId == dbSong.PlaylistId))
            {
                return null;
            }
            context.Dbsongs.Add(dbSong);
            context.SaveChanges();
            return dbSong;
        }
        public Dbsong RemoveSongFromPlaylist(string id, int songID, string listTitle)

        {
            User user = context.Users.FirstOrDefault(u => u.GoogleId == id);
            Playlist playlist = context.Playlists.FirstOrDefault(p => p.ListTitle == listTitle && p.UserId == user.Id);
            Dbsong song = context.Dbsongs.FirstOrDefault(s => s.Id == songID);
            if(song != null)
            {
                context.Remove(song);
                context.SaveChanges();
                return song;
            }
            else
            {
                return null;
            }
        }

        //SONG Methods
        public Dbsong GetDBSongDetails(string id, string songID, string listTitle)
        {
            User user = context.Users.FirstOrDefault(u => u.GoogleId == id);
            Playlist playlist = context.Playlists.FirstOrDefault(p => p.ListTitle == listTitle && p.UserId == user.Id);
            return context.Dbsongs.FirstOrDefault(s => s.Apiid == songID);
        }
        public Dbsong TransposeUp(string apiid, string listTitle, string newKey, string id)
        {
            User user = context.Users.FirstOrDefault(u => u.GoogleId == id);
            Playlist playlist = context.Playlists.FirstOrDefault(p => p.ListTitle == listTitle && p.UserId == user.Id);
            Dbsong song = context.Dbsongs.FirstOrDefault(s => s.Apiid==apiid && s.PlaylistId==playlist.Id);
            song.TransposedKey = newKey;
            context.Dbsongs.Update(song);
            context.SaveChanges();
            return song;

        }
        public Dbsong TransposeDown(string apiid, string listTitle, string newKey, string id)
        {
            User user = context.Users.FirstOrDefault(u => u.GoogleId == id);
            Playlist playlist = context.Playlists.FirstOrDefault(p => p.ListTitle == listTitle && p.UserId == user.Id);
            Dbsong song = context.Dbsongs.FirstOrDefault(s => s.Apiid==apiid && s.PlaylistId==playlist.Id);
            if (song != null)
            {
                song.TransposedKey = newKey;
                context.SaveChanges();
                return song;
            }
            else
            {
                return null;
            }
        }
    }
}
