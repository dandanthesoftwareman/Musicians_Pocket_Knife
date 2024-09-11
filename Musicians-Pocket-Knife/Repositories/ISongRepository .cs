using Musicians_Pocket_Knife.Models;

namespace Musicians_Pocket_Knife.Repositories
{
    public interface ISongRepository
    {
        Dbsong AddSongToPlaylist(string id, APISong song, int listId);

        Dbsong AddSongToPlaylist(string id, APISong song, int listId, bool addDuplicate);

        Dbsong GetDBSongDetails(string id, string songID, string listTitle);

        Dbsong RemoveSongFromPlaylist(string id, int songID, int listId);

        Task SaveTransposeChanges(List<Dbsong> songs);

        void UpdateSongIndexes(List<Dbsong> songs);
    }
}