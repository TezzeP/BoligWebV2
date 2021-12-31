using Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        //private IUserService _userService;
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;       

        public AuthController(/*IUserService userService,*/ UserManager<IdentityUser> userManager, 
                                SignInManager<IdentityUser> signInManager)
        {
            //_userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;

           
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

         // /api/auth/login 
        [HttpPost("Login")]    
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return Ok(result); // Status Code: 200 
                }
                return BadRequest(result);
            }
            return BadRequest("Some properties are not valid"); // Status Code 400 Internal
           
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Ok(result); // Status Code: 200 
                }                
                return BadRequest(result);
            }
            return BadRequest("Some properties are not valid"); // Status Code 400 Internal
        }



        // VIRKER -->
        // /api/auth/register
        //[HttpPost("Register")]
        //public async Task<IActionResult> RegisterAsync([FromBody]RegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _userService.RegisterUserAsync(model);
        //        if (result.IsSuccess)
        //            return Ok(result); // Status Code: 200 

        //        return BadRequest(result);


        //    }
        //    return BadRequest("Some properties are not valid"); // Status Code 400 Internal
        //}


        // /api/auth/login // Virker
        //[HttpPost("Login")]
        //public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _userService.LoginUserAsync(model);

        //        if (result.IsSuccess)
        //        {

        //            return Ok(result);
        //        }

        //        return BadRequest(result);
        //    }

        //    return BadRequest("Some properties are not valid");
        //}
    }
}
