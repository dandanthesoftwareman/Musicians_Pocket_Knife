using Musicians_Pocket_Knife.Models;

namespace Musicians_Pocket_Knife.Orchestrators
{
    public interface IUserOrchestrator
    {
        Task<User?> CreateNewUserAsync(User user);
    }
}
