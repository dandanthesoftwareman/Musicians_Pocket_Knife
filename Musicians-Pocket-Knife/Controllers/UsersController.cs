using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Musicians_Pocket_Knife.Models;

namespace Musicians_Pocket_Knife.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        MpkdbContext context = new MpkdbContext();

        [HttpPost("CreateNewUser")]
        public User CreateNewUser(string googleId, string name)
        {
            if (context.Users.Any(x => x.GoogleId == googleId))
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
                context.Users.Add(newUser);
                context.SaveChanges();
                return newUser;
            }
        }
    }
}
