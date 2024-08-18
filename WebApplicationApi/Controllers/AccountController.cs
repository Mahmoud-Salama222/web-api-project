using Core.Entity.identity;
using Core.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using WebApplicationApi.Dto;
using WebApplicationApi.Error;

namespace WebApplicationApi.Controllers
{

    public class AccountController : ApibaseController
    {
        private readonly UserManager<Appuser> userManager;
        private readonly SignInManager<Appuser> signInManager;
        private readonly ITokenservices tokenservices;

        public AccountController(UserManager<Appuser> UserManager,SignInManager<Appuser> SignInManager,ITokenservices tokenservices) {
            userManager = UserManager;
            signInManager = SignInManager;
            this.tokenservices = tokenservices;
        }



        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            var user = new Appuser()
            {
                Displayname = model.Displayname,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.phonenumber

            };
            var result=await userManager.CreateAsync(user,model.Password);
            if(!result.Succeeded){ return BadRequest(new Apierrorresponce(400)); }
            return Ok(new UserDto()
            {
                Displayname = user.Displayname,
                Email = user.Email,
                Token = await tokenservices.creattokenasync(user, userManager)
            });
        }
    
        [HttpPost("login")]  //Api/Account/login
        public async Task<ActionResult<UserDto>> login(LoginDto model) //LoginDto:responce shape from frontend and i will return to him userdto
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null) { return Unauthorized(new Apierrorresponce(401)); }
            var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded) return Unauthorized(new Apierrorresponce(401));
            return Ok(new UserDto()
            {
                Displayname = user.Displayname,
                Email = user.Email,
                Token = await tokenservices.creattokenasync(user, userManager)
            });

        }

             [HttpGet("Getcurentuser")]  //Api/Account/login
        public async Task<ActionResult<UserDto>> Getcurentuser() //LoginDto:responce shape from frontend and i will return to him userdto
        {
           var email=User.FindFirstValue(ClaimTypes.Email);

            var user = await userManager.FindByEmailAsync(email);

            return Ok(new UserDto()
            {
                Displayname = user.Displayname,
                Email = user.Email,
                Token = await tokenservices.creattokenasync(user, userManager)
            }) ;

        }
       
        
        [Authorize]
        [HttpGet("adress")]  //Api/Account/login
        public async Task<ActionResult<UserDto>> Getuseradress() //LoginDto:responce shape from frontend and i will return to him userdto
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await userManager.FindByEmailAsync(email);

            return Ok(new UserDto()
            {
                Displayname = user.Displayname,
                Email = user.Email,
                Token = await tokenservices.creattokenasync(user, userManager)
            });

        }



    }
}
