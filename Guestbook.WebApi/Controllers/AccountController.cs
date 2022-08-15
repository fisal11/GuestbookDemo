using GB.Services.Interfaces;
using GB.Services.Model;
using Microsoft.AspNetCore.Mvc;

namespace Guestbook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccount _account;

        public AccountController(IAccount account)
        {
            _account = account;
        }


        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModel)
        {
            var result = await _account.SignUp(signUpModel);

            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return BadRequest();
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel signInModel)
        {
            var result = await _account.SignIn(signInModel);

            if (string.IsNullOrEmpty(result))
            {

                return Unauthorized();
            }
            return Ok(result);
        }

    }
}
