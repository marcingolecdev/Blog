using Blog.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager <IdentityUser> _userManager;
        private readonly SignInManager <IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

         

        public AccountController (UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
             
            
        }



        /*
         As the name suggests, the method adds new users to the database, works with the "Register.razor" component
         where there are "Input" fields from which data is collected from the user to the "RegisterRequest" class,
         then its parameters are passed to the "user" variable, which is the "IdentityUser" class, after which it is added
         to "CreateAsync()" along with the code. The "CreateAsync()" method is provided by the "UserManager" from the package
         Microsoft.AspNetCore.Identity that was installed earlier.
         If the operation of adding a user is successful, the role "User" is assigned to "user". diff
         here have been created to allow registered users to access the relevant parts of the Blog.
         However, if the registration fails, a server-side error message is returned to the function.
         Such a message may occur when, for example, a user with this name already exists.*/
        [HttpPost]
        [Route("register")] 
        public async Task<IActionResult> Register ([FromBody] RegisterRequest register)
        {
            string userGuid = Guid.NewGuid ().ToString ();
            IdentityUser user = new IdentityUser ()
            {
                Id = userGuid,
                ConcurrencyStamp = Guid.NewGuid ().ToString (),
                SecurityStamp = Guid.NewGuid ().ToString (),
                Email = register.Email,
                UserName = register.Email
            };
            var result = await _userManager.CreateAsync (user, register.Password);
            if (result.Succeeded)
            {
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "User"));
                await _userManager.AddToRoleAsync(user, "User");
                return Ok(user);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }

        }

        /*
        The next method below is responsible for logging in to the website.
         When logging in, there are two possible scenarios to obtain,
         the first is positive login and the second is unauthorized access and occurs then
         when the login user enters an incorrect login or password.
        
         This method also contains the "GenerateJwtToken()" method, which contains the code responsible for
         generating new tokens that are sent between the client and the server
         when logging in. We see that the method has a LoginRequest, its class looks like this
         public class LoginRequest
         {
             public string Email { get; set; }
             public string Password { get; set; }
         }
         and tells what fields are to be fetched from the logging user, then it is checked
         whether the logging user is in the database, if not, the null value is returned, and thus
         you will not be logged in. If the user is in the database, he will log in via the method
         "PasswordSignInAsync()". "isPersistent" tells whether to keep the logged in user in memory,
         this means that the next time you return to the site, you will be immediately redirected to the authorized content
         without logging in. "lockoutOnFailure" tells whether the account should be locked after entering an incorrect password.
         In the next line, the user's role is retrieved, and below, data about the logged-in user is saved to the "UserSession" class
         along with encrypted


         The "Login" method also contains two attributes "HttpPost" and "Route("Login")".
         The "Route" attribute is part of the API that the client application calls in the "Login.razor" component.
         "[HttpPost]" says that this is the method responsible for transferring data to the database.*/

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserSession>> Login ([FromBody] LoginRequest loginRequest)
        {
            var userSession = await GenerateJwtToken (loginRequest);
            if (userSession is null)
            {
                return Unauthorized();
            }
            else
            {
                return userSession;
            }
        }

         
        private async Task<UserSession> GenerateJwtToken (LoginRequest loginRequest)
        {
            var userAccount = await _userManager.FindByEmailAsync (loginRequest.Email);
            if (userAccount != null)
            {
                if ((await _signInManager.PasswordSignInAsync(userAccount, loginRequest.Password, isPersistent:false, lockoutOnFailure:false)).Succeeded)
                {
                    string role = (await _userManager.GetRolesAsync(userAccount)).FirstOrDefault ();


                    var tokenExpiryTimeStamp = DateTime.Now.AddMinutes (Convert.ToDouble(_configuration["JWT:TokenValid"]));
                    var tokenKey = Encoding.ASCII.GetBytes (_configuration["JWT:SecurityKey"]);
                    var claimsIdentity = new ClaimsIdentity (new List <Claim> ()
                    {
                        new Claim (ClaimTypes.Name, userAccount.UserName),
                        new Claim (ClaimTypes.Role, userAccount.Email)
                    });

                    var signingCredentials = new SigningCredentials (
                        new SymmetricSecurityKey (tokenKey),
                        SecurityAlgorithms.HmacSha256Signature);

                    var securityTokenDescriptor = new SecurityTokenDescriptor ()
                    {
                        Subject = claimsIdentity,
                        Expires = tokenExpiryTimeStamp,
                        SigningCredentials = signingCredentials
                    };

                    var jwtSecurityTokenHandler = new JwtSecurityTokenHandler ();
                    var securityToken = jwtSecurityTokenHandler.CreateToken (securityTokenDescriptor);
                    var token = jwtSecurityTokenHandler.WriteToken (securityToken);

                    var userSession = new UserSession
                    {
                        UserName = userAccount.UserName,
                        Role = role,
                        Token = token,
                        Expiration = (int) tokenExpiryTimeStamp.Subtract (DateTime.Now).TotalSeconds
                    };

                    return userSession;
                }

            }
            return null;
        }


    }
}
