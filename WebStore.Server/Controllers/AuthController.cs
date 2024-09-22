using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStore.Server.Interfaces;
using WebStore.Server.Models;
using WebStore.Server.Models.DTOs;

namespace WebStore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
        {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        
            public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService)
            {
                _userManager = userManager;
                _tokenService = tokenService;
                _signInManager = signInManager;
            }

            [HttpPost("register")]
            public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }
                    var user = new ApplicationUser
                    {
                        Name = dto.Name,
                        UserName = dto.Email,
                        Email = dto.Email
                    };
                    var createUser = await _userManager.CreateAsync(user, dto.Password);
                    if (createUser.Succeeded)
                    {
                        var roleResult = await _userManager.AddToRoleAsync(user, "Customer");
                        if (roleResult.Succeeded)
                        {
                            var newUser = new UserAuthResDTO
                            {
                                Name = user.Name,
                                Email = user.Email,
                                Token = _tokenService.CreateToken(user)
                            };
                            return Ok(newUser);
                        }else
                        {
                            return StatusCode(500, roleResult.Errors);
                        }
                    }else
                    {
                        return StatusCode(500, createUser.Errors);
                    }
                }catch (Exception ex)
                {
                    return StatusCode(500, ex);
                }
              
            }

            [HttpPost("login")]
            public async Task<IActionResult> Login(LoginDTO dto)
            {
                if (!ModelState.IsValid)
                { 
                    return BadRequest(ModelState);
                }
                var user = await _userManager.Users.FirstOrDefaultAsync(e => e.Email == dto.Email);
                if (user == null)
                {
                    return Unauthorized("Invalid email");
                }
                var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
                if (!result.Succeeded)
                {
                    return Unauthorized("Email not found and/or password incorrect");
                }
                var userLogin = new UserAuthResDTO
                {
                    Name = user.Name,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                };
            return Ok(userLogin);
            }

            [HttpPost("logout")]
            public IActionResult Logout()
            {
                return BadRequest();
            }
        }
}
