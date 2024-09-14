using Microsoft.EntityFrameworkCore;
using Musicians_Pocket_Knife.Models;
using Newtonsoft.Json;

namespace Musicians_Pocket_Knife.Repositories
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly MpkdbContext _context;

        public PlaylistRepository(MpkdbContext context)
        {
            _context = context;
        }

        public Playlist CreatePlaylist(string listTitle, string id)
        {
            User user = _context.Users.FirstOrDefault(u => u.GoogleId == id);
            Playlist playlist = new Playlist()
            {
                ListTitle = listTitle,
                UserId = user.UserId,
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

        public void UpdateDateViewed(int listId, string id)
        {
            _context.Playlists.FirstOrDefault(p => p.Id == listId && p.User.GoogleId == id).LastDateViewed = DateTime.Now;
            _context.SaveChanges();
        }
    }
}
