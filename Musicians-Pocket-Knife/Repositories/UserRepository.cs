using Musicians_Pocket_Knife.Models;

namespace Musicians_Pocket_Knife.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MpkdbContext _context;

        public UserRepository(MpkdbContext context)
        {
            this._context = context;
        }

        public User CreateNewUser(string googleId, string name)
        {
            if (_context.Users.Any(x => x.GoogleId == googleId))
            {
                return null;
            }
            else
            {
                User newUser = new User()
                {
                    FirstName = name.Split('_')[0].ToString(),
                    LastName = name.Split('_')[1].ToString(),
                    GoogleId = googleId
                };
                _context.Users.Add(newUser);
                _context.SaveChanges();
                return newUser;
            }
        }
    }
}
