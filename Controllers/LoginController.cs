using Shared.Project.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly Imitarbeiterdevicelogininterface _accountInterface;

        public LoginController(Imitarbeiterdevicelogininterface accountInterface)
        {
            _accountInterface = accountInterface;
        }

       
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> SignInAsync(Login user)
        {
            if (user == null) return BadRequest("Model is empty");
            var result = await _accountInterface.SignInAsync(user);
            return Ok(result);
        }

        //[HttpGet("user-image/{mandant}")]
        //[Authorize]
        //public async Task<IActionResult> GetUserImage(int mandant)
        //{
        //    var result = await _accountInterface.GetUserImage(mandant);
        //    return Ok(result);
        //}

        //[HttpPut("update-profile")]
        //[Authorize]
        //public async Task<IActionResult> UpdateProfile(UserProfile profile)
        //{
        //    var result = await _accountInterface.UpdateProfile(profile);
        //    return Ok(result);
        //}
    }
}
