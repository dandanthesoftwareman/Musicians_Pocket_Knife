using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Musicians_Pocket_Knife.Models;
using System.Data;

namespace Musicians_Pocket_Knife.Repositories
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly MpkdbContext _context;
        private readonly string CreatePlaylistStoredProcedure = "[MPKDB].[dbo].[CreateNewPlaylist]";
        private readonly string _connectionString;

        public PlaylistRepository(MpkdbContext context, string connectionString)
        {
            _context = context;
            _connectionString = connectionString;
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<bool> CreatePlaylist(CreateNewPlaylistRequest request)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new
                {
                    GoogleId = request.UserId,
                    ListTitle = request.ListTitle
                };

                try
                {
                    await connection.ExecuteAsync(CreatePlaylistStoredProcedure, parameters, commandType: CommandType.StoredProcedure);
                    return true;
                }
                catch (SqlException ex)
                {
                    if (ex.Message.Contains("A playlist with this title already exists"))
                    {
                        Console.WriteLine("Playlist already exists for the user.");
                    }
                    else if (ex.Message.Contains("User with the provided GoogleId does not exist"))
                    {
                        Console.WriteLine("User does not exist.");
                    }

                    return false;
                }
            }
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
