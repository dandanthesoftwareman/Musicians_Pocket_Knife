using Microsoft.EntityFrameworkCore;
using Musicians_Pocket_Knife.Models;
using Newtonsoft.Json;

namespace Musicians_Pocket_Knife.Repositories
{
    public class DBRepository : IDBRepository
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
            return _context.Dbsongs.Where(s => s.Playlist.Id == listId && s.Playlist.User.GoogleId == id).OrderBy(s => s.SongIndex).ToList();
        }
        public Dbsong AddSongToPlaylist(string id, APISong song, int listId)
        {
            return AddSongToPlaylist(id, song, listId, false);
        }
        public Dbsong AddSongToPlaylist(string id, APISong song, int listId, bool addDuplicate)
        {
            Playlist playlist = _context.Playlists.FirstOrDefault(p => p.Id == listId && p.User.GoogleId == id);
            List<Dbsong> songCount = _context.Dbsongs.Where(s => s.PlaylistId == listId).ToList();
            Dbsong dbSong = new Dbsong()
            {
                SongIndex = songCount.Count(),
                PlaylistId = playlist.Id,
                Title = song.song.title,
                Apiid = song.song.id,
                Artist = song.song.artist.name,
                Tempo = song.song.tempo,
                TimeSignature = song.song.time_sig,
                OriginalKey = song.song.key_of,
                TransposedKey = song.song.key_of
            };
            if (addDuplicate == false && _context.Dbsongs.Any(x => x.Apiid == song.song.id && x.PlaylistId == dbSong.PlaylistId))
            {
                return null;
            }
            _context.Dbsongs.Add(dbSong);
            _context.SaveChanges();
            return dbSong;
        }
        public Dbsong RemoveSongFromPlaylist(string id, int songID, int listId)
        {
            Dbsong song = _context.Dbsongs.FirstOrDefault(s => s.Id == songID &&
            s.Playlist.Id == listId && s.Playlist.User.GoogleId == id);
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
        public void UpdateDateViewed(int listId, string id)
        {
            _context.Playlists.FirstOrDefault(p => p.Id == listId && p.User.GoogleId == id).LastDateViewed = DateTime.Now;
            _context.SaveChanges();
        }

        //SONG Methods
        public Dbsong GetDBSongDetails(string id, string songID, string listTitle)
        {
            return _context.Dbsongs.FirstOrDefault(s => s.Apiid == songID && s.Playlist.ListTitle == listTitle && s.Playlist.User.GoogleId == id);
        }
        public async Task SaveTransposeChanges(List<Dbsong> songs)
        {
            //var tasks = songs.Select(s => UpdateDbSong(s));
            songs.ForEach(s => UpdateDbSong(s));
            //await Task.WhenAll(tasks);
            _context.SaveChanges();
        }
        private void UpdateDbSong(Dbsong s)
        {
            Dbsong dbsong = _context.Dbsongs.FirstOrDefault(x => x.Apiid == s.Apiid && x.PlaylistId == s.PlaylistId);
            if (dbsong.TransposedKey != s.TransposedKey)
            {
                dbsong.TransposedKey = s.TransposedKey;
                _context.Update(dbsong);
            }
        }
        public void UpdateSongIndexes(List<Dbsong> songs)
        {
            if (songs.Count > 0)
            {
                foreach (Dbsong song in songs)
                {
                    Dbsong s = _context.Dbsongs.FirstOrDefault(s => s.Apiid == song.Apiid && s.PlaylistId == song.PlaylistId);
                    if (s.SongIndex != song.SongIndex)
                    {
                        s.SongIndex = song.SongIndex;
                        _context.Update(s);
                        _context.SaveChanges();
                    }
                }
            }
        }
    }
}
