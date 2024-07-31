using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using myproject.Models.DTO;
using myproject.Repositroies.Interface;

namespace myproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenInterface tokenInterface;

        public AuthController(UserManager<IdentityUser> userManager,ITokenInterface tokenInterface)
        {
            this.userManager = userManager;
            this.tokenInterface = tokenInterface;
        }


        [HttpPost]
        [Route("login")]

        public async Task<IActionResult> login([FromBody] loginRequestDto request)
        {
            var identityUser = await userManager.FindByEmailAsync(request.email);
            if(identityUser!=null)
            {

                var checkPasswordResult = await userManager.CheckPasswordAsync(identityUser,request.password);
                if(checkPasswordResult)
                {

                    var getrole = await userManager.GetRolesAsync(identityUser);
                    var token=  tokenInterface.createJwtToken(identityUser,getrole.ToList());
                    var response = new loginResponseDto
                    {
                        email = request.email,
                        roles=getrole.ToList(),
                        token=token

                    };
                    return Ok(response);
                }
                else
                {
                    ModelState.AddModelError("", "password or id is incorrect");
                    return ValidationProblem(ModelState);
                }
            }
            ModelState.AddModelError("", "user does not exist");
            return ValidationProblem(ModelState);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequestDto request)
        {
            var user = new IdentityUser {
               UserName=request.Email.Trim(),
               Email=request.Email.Trim()
            };

           var identityresult= await userManager.CreateAsync(user,request.Password);
            if(identityresult.Succeeded)
            {
                //add role to user
                 identityresult= await userManager.AddToRoleAsync(user, "Reader");
                if(identityresult.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    if (identityresult.Errors.Any())
                    {
                        foreach (var error in identityresult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }

            }
            else
            {
                if (identityresult.Errors.Any())
                {
                    foreach (var error in identityresult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return ValidationProblem(ModelState);

        }
    }
}
