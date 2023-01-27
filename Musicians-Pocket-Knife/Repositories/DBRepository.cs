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
            if (context.Playlists.Any(x => x.ListTitle == listTitle))
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
        public Playlist RenamePlaylist(string oldTitle, string newTitle, string id)
        {
            User user = context.Users.FirstOrDefault(u => u.GoogleId == id);
            Playlist playlist = context.Playlists.FirstOrDefault(p => p.ListTitle == oldTitle && p.UserId == user.Id);
            playlist.ListTitle = newTitle;
            context.Update(playlist);
            context.SaveChanges();
            return playlist;
        }
        public Playlist DeletePlaylist(string listTitle, string id)
        {
            User user = context.Users.FirstOrDefault(u => u.GoogleId == id);
            Playlist playlist = context.Playlists.FirstOrDefault(x => x.ListTitle == listTitle && x.UserId == user.Id);
            List<Dbsong> songs = context.Dbsongs.Where(x => x.PlaylistId == playlist.Id).ToList();
            if (songs != null)
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
            return context.Playlists.Where(u => u.User.GoogleId == id).ToList();
        }
        public List<Dbsong> ViewPlaylistDetails(string title, string id)
        {
            return context.Dbsongs.Where(s => s.Playlist.ListTitle == title && s.Playlist.User.GoogleId == id).ToList();
        }
        public Dbsong AddSongToPlaylist(string id, APISong song, string listTitle)
        {
            Playlist playlist = context.Playlists.FirstOrDefault(p => p.ListTitle == listTitle && p.User.GoogleId == id);
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
            if (context.Dbsongs.Any(x => x.Apiid == song.song.id && x.PlaylistId == dbSong.PlaylistId))
            {
                return null;
            }
            context.Dbsongs.Add(dbSong);
            context.SaveChanges();
            return dbSong;
        }
        public Dbsong RemoveSongFromPlaylist(string id, int songID, string listTitle)

        {
            Dbsong song = context.Dbsongs.FirstOrDefault(s => s.Id == songID &&
            s.Playlist.ListTitle == listTitle && s.Playlist.User.GoogleId == id);
            if (song != null)
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
            return context.Dbsongs.FirstOrDefault(s => s.Apiid == songID && s.Playlist.ListTitle == listTitle && s.Playlist.User.GoogleId == id);
        }
        public void SaveTransposeChanges(List<Dbsong> songs)
        {
            foreach (Dbsong s in songs)
            {
                Dbsong dbsong = context.Dbsongs.FirstOrDefault(x => x.Apiid == s.Apiid && x.PlaylistId == s.PlaylistId);
                if (dbsong.TransposedKey != s.TransposedKey)
                {
                    dbsong.TransposedKey = s.TransposedKey;
                    context.Update(dbsong);
                }
            }
            context.SaveChanges();
        }
    }
}
