using Microsoft.EntityFrameworkCore;
using Musicians_Pocket_Knife.Models;
using Newtonsoft.Json;

namespace Musicians_Pocket_Knife.Repositories
{
    public class DBRepository
    {
        public DBRepository(MpkdbContext context)
        {
            _context = context;
        }
        MpkdbContext _context;

        //PLAYLIST Methods
        public Playlist CreatePlaylist(string listTitle, string id)
        {
            User user = _context.Users.FirstOrDefault(u => u.GoogleId == id);
            Playlist playlist = new Playlist()
            {
                ListTitle = listTitle,
                UserId = user.Id,
                DateCreated = DateTime.Now,
                LastDateViewed = DateTime.Now
            };
            if (_context.Playlists.Any(x => x.ListTitle == listTitle))
            {
                return null;
            }
            else
            {
                _context.Add(playlist);
                _context.SaveChanges();
                return playlist;
            }
        }
        public Playlist RenamePlaylist(int listId, string newTitle, string id)
        {
            Playlist playlist = _context.Playlists.FirstOrDefault(p => p.Id == listId && p.User.GoogleId == id);
            playlist.ListTitle = newTitle;
            _context.Update(playlist);
            _context.SaveChanges();
            return playlist;
        }
        public void DeletePlaylist(int listId, string id)
        {
            Playlist playlist = _context.Playlists.FirstOrDefault(x => x.Id == listId && x.User.GoogleId == id);
            List<Dbsong> songs = _context.Dbsongs.Where(x => x.PlaylistId == playlist.Id).ToList();
            if (songs != null)
            {
                foreach (Dbsong s in songs)
                {
                    _context.Remove(s);
                }
            }
            _context.Remove(playlist);
            _context.SaveChanges();
        }
        public List<Playlist> GetUserPlaylists(string id)
        {
            return _context.Playlists.Where(u => u.User.GoogleId == id).ToList();
        }
        public Playlist GetListTitle(int listId, string id)
        {
            return _context.Playlists.FirstOrDefault(p => p.Id==listId && p.User.GoogleId == id);
        }
        public List<Dbsong> ViewPlaylistDetails(int listId, string id)
        {
            return _context.Dbsongs.Where(s => s.Playlist.Id == listId && s.Playlist.User.GoogleId == id).ToList();
        }
        public Dbsong AddSongToPlaylist(string id, APISong song, string listTitle)
        {
            Playlist playlist = _context.Playlists.FirstOrDefault(p => p.ListTitle == listTitle && p.User.GoogleId == id);
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
            if (_context.Dbsongs.Any(x => x.Apiid == song.song.id && x.PlaylistId == dbSong.PlaylistId))
            {
                return null;
            }
            _context.Dbsongs.Add(dbSong);
            _context.SaveChanges();
            return dbSong;
        }
        public Dbsong RemoveSongFromPlaylist(string id, int songID, string listTitle)

        {
            Dbsong song = _context.Dbsongs.FirstOrDefault(s => s.Id == songID &&
            s.Playlist.ListTitle == listTitle && s.Playlist.User.GoogleId == id);
            if (song != null)
            {
                _context.Remove(song);
                _context.SaveChanges();
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
            return _context.Dbsongs.FirstOrDefault(s => s.Apiid == songID && s.Playlist.ListTitle == listTitle && s.Playlist.User.GoogleId == id);
        }
        public void SaveTransposeChanges(List<Dbsong> songs)
        {
            foreach (Dbsong s in songs)
            {
                Dbsong dbsong = _context.Dbsongs.FirstOrDefault(x => x.Apiid == s.Apiid && x.PlaylistId == s.PlaylistId);
                if (dbsong.TransposedKey != s.TransposedKey)
                {
                    dbsong.TransposedKey = s.TransposedKey;
                    _context.Update(dbsong);
                }
            }
            _context.SaveChanges();
        }
    }
}
