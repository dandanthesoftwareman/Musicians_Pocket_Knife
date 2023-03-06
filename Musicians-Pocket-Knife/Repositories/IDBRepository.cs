using Musicians_Pocket_Knife.Models;

namespace Musicians_Pocket_Knife.Repositories
{
    public interface IDBRepository
    {
        Dbsong AddSongToPlaylist(string id, APISong song, int listId);
        Dbsong AddSongToPlaylist(string id, APISong song, int listId, bool addDuplicate);
        Playlist CreatePlaylist(string listTitle, string id);
        void DeletePlaylist(int listId, string id);
        Dbsong GetDBSongDetails(string id, string songID, string listTitle);
        Playlist GetListTitle(int listId, string id);
        List<Playlist> GetUserPlaylists(string id);
        Dbsong RemoveSongFromPlaylist(string id, int songID, string listTitle);
        Playlist RenamePlaylist(int listId, string newTitle, string id);
        Task SaveTransposeChanges(List<Dbsong> songs);
        void UpdateDateViewed(int listId, string id);
        List<Dbsong> ViewPlaylistDetails(int listId, string id);
    }
}