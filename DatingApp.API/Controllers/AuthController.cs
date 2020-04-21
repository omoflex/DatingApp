using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _config = config;
            _repo = repo;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserForRrgisterDto userForRrgisterDto)
        {
            //Validate request

            // if(!ModelState.IsValid)
            // return BadRequest(ModelState);

            userForRrgisterDto.Username = userForRrgisterDto.Username.ToLower();

            if (await _repo.UserExists(userForRrgisterDto.Username))
            {
                return BadRequest("The Username already exist");
            }

            var userToCreate = new User()
            {
                Username = userForRrgisterDto.Username
            };
            var userCreated = await _repo.Register(userToCreate, userForRrgisterDto.Password);
            return StatusCode(201);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            //Validate request

            //  try
            //  {
           
            var userFromRepo = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

            if (userFromRepo == null)
              //return Unauthorized();
              return StatusCode(404,"Invalid login credentials.");

            var claims = new[]{
              new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
              new Claim(ClaimTypes.Name,userFromRepo.Username)

            };
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds= new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor= new SecurityTokenDescriptor{
                 Subject= new ClaimsIdentity(claims),
                  Expires= DateTime.Now.AddDays(1),
                   SigningCredentials=creds
            };

            var tokenHandler= new JwtSecurityTokenHandler();
            var token=tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new {
               token=tokenHandler.WriteToken(token)         
            });
            //  }
            //  catch (System.Exception)
            //  {
                 
            //      return StatusCode(500,"Computer Really says no");
            //  }
            
        }
    }
}