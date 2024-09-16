using Musicians_Pocket_Knife.Models;

namespace Musicians_Pocket_Knife.Repositories
{
    public interface IPlaylistRepository
    {
        Task<bool> CreatePlaylist(CreateNewPlaylistRequest request);

        void DeletePlaylist(int listId, string id);

        Playlist GetListTitle(int listId, string id);

        List<Playlist> GetUserPlaylists(string id);

        Playlist RenamePlaylist(int listId, string newTitle, string id);

        void UpdateDateViewed(int listId, string id);

        List<Dbsong> ViewPlaylistDetails(int listId, string id);
    }
}