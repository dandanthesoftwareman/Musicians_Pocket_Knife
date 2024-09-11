using Musicians_Pocket_Knife.Models;

namespace Musicians_Pocket_Knife.Repositories
{
    public interface IUserRepository
    {
        public User CreateNewUser(string googleId, string name);
    }
}
