using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Musicians_Pocket_Knife.Models;

namespace Musicians_Pocket_Knife.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public UsersController(MpkdbContext context)
        {
            _context = context;
        }
        MpkdbContext _context;

        [HttpPost("CreateNewUser")]
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
