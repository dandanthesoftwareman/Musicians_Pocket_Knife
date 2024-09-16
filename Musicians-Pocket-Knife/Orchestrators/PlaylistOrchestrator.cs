using Musicians_Pocket_Knife.Models;
using Musicians_Pocket_Knife.Repositories;

namespace Musicians_Pocket_Knife.Orchestrators
{
    public class PlaylistOrchestrator : IPlaylistOrchestrator
    {
        private readonly IPlaylistRepository _playlistRepository;

        public PlaylistOrchestrator(IPlaylistRepository playlistRepository)
        {
            _playlistRepository = playlistRepository;
        }

        public async Task<bool> CreateNewPlaylist(CreateNewPlaylistRequest request)
        {
            return await _playlistRepository.CreatePlaylist(request);
        }
    }
}
