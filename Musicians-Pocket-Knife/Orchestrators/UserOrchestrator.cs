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

        public bool? VerifyExistingUser(VerifyExistingUserRequest request)
        {
            if (request.isActive && request.GoogleId != null)
            {
                return userRepository.VerifyExistingUser(request);
            }

            return null;
        }

        public async Task<User?> CreateNewUserAsync(User newUserRequest)
        {
            var user = await userRepository.CreateNewUserAsync(newUserRequest);
            return user;
        }
    }
}
