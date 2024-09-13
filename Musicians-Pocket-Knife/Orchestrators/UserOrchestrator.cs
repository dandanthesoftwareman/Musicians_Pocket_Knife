using Musicians_Pocket_Knife.Models;
using Musicians_Pocket_Knife.Repositories;

namespace Musicians_Pocket_Knife.Orchestrators
{
    public class UserOrchestrator : IUserOrchestrator
    {
        private IUserRepository userRepository;

        public UserOrchestrator(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User?> CreateNewUserAsync(User newUserRequest)
        {
            var user = await userRepository.CreateNewUserAsync(newUserRequest);
            return user;
        }
    }
}
