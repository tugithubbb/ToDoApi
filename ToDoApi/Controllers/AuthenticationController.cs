using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.DTOs.Request;
using ToDoApi.Models;
using ToDoApi.Services;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAuthenticationService iauthenticationService;
        public AuthenticationController(
        UserManager<ApplicationUser> userManager, IAuthenticationService iauthenticationService
        )
        {
            this.userManager = userManager;
            this.iauthenticationService = iauthenticationService;
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            
            var userRequest = await userManager.FindByNameAsync(model.Username);
            if (userRequest == null)
                return Unauthorized("Username hoặc password không đúng.");

            
            var valid = await userManager.CheckPasswordAsync(userRequest, model.Password);
            if (!valid)
                return Unauthorized("Username hoặc password không đúng.");

            
            var token = await iauthenticationService.GenerateTokenAsync(userRequest);

            
            return Ok(new
            {
                token,
                expires = DateTime.UtcNow.AddHours(
                            double.Parse(
                              HttpContext.RequestServices
                                         .GetRequiredService<IConfiguration>()["Jwt:ExpireHours"]
                            )
                          )
            });
        }


    }
}
