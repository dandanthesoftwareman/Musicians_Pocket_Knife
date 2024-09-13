using Musicians_Pocket_Knife.Models;

namespace Musicians_Pocket_Knife.Repositories
{
    public interface IUserRepository
    {
        Task<User?> CreateNewUserAsync(User createUserRequest);
    }
}
