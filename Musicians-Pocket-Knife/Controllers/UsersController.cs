using Microsoft.AspNetCore.Mvc;
using Musicians_Pocket_Knife.Models;
using Musicians_Pocket_Knife.Orchestrators;
using AutoMapper;

namespace Musicians_Pocket_Knife.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserOrchestrator userOrchestrator;
        private readonly IMapper mapper;

        public UsersController(IUserOrchestrator userOrchestrator, IMapper mapper)
        {
            this.userOrchestrator = userOrchestrator;
            this.mapper = mapper;
        }

        [HttpPost("VerifyExistingUser")]
        public IActionResult VerifyExistingUser(VerifyExistingUserRequest request)
        {
            var response = userOrchestrator.VerifyExistingUser(request);

            return Ok(response);
        }

        [HttpPost("CreateNewUser")]
        public async Task<IActionResult> CreateNewUser(CreateNewUserRequest request)
        {
            try
            {
                var newUser = mapper.Map<User>(request);
                var response = await userOrchestrator.CreateNewUserAsync(newUser);

                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
