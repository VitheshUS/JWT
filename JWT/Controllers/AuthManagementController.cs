using JWT.Configurations;
using JWT.Models.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthManagementController:ControllerBase
    {
        private readonly ILogger<AuthManagementController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtConfig _jwtConfig;
        public AuthManagementController(
            ILogger<AuthManagementController> logger, 
            UserManager<IdentityUser> userManager,
            IOptionsMonitor<JwtConfig> optionsMonitor
            )
        {
            _logger = logger;
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]UserRegistrationRequestDto userRegistrationRequestDto)
        {
           if(!ModelState.IsValid)
                return BadRequest("Invalid Request");

            var existingUser =await _userManager.FindByEmailAsync(userRegistrationRequestDto.Email);

            if(existingUser != null)
                return BadRequest("Email already exists");

            var newUser = new IdentityUser
            {
                UserName=userRegistrationRequestDto.Name,
                Email= userRegistrationRequestDto.Email,
            };
            var isCreated=await _userManager.CreateAsync(newUser,userRegistrationRequestDto.Password);

            if (isCreated.Succeeded)
            {
                return Ok(new RegistrationResponse
                {
                    Result=true,
                    Token=""
                });
            }
            else
                return BadRequest("Error while creating the user");
        }
        //private string GenerateJwtToken(IdentityUser User)
        //{
        //    var jwtTokenHandler=new JwtSecurityTokenHandler();

        //    var key=Encoding.ASCII.GetBytes(_jwtConfig.Secret);

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject=new System.Security.Claims.ClaimsIdentity(new []
        //        {
        //            new Claim("Id",User.Id),
        //            new Claim(JwtRegisteredClaimNames.Sub,User.Email!),
        //            new Claim(JwtRegisteredClaimNames.Email,User.Email!),
        //            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
        //        }),
        //        Expires=DateTime.Now.AddDays(1),
        //        SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha512)
        //    };
        //}

    }
}
