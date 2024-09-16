using Musicians_Pocket_Knife.Models;

namespace Musicians_Pocket_Knife.Orchestrators
{
    public interface IPlaylistOrchestrator
    {
        Task<bool> CreateNewPlaylist(CreateNewPlaylistRequest request);
    }
}
