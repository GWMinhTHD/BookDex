using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Server.Extensions;
using WebStore.Server.Interfaces;
using WebStore.Server.Models;
using WebStore.Server.Models.DTOs;

namespace WebStore.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        public ProfileController(UserManager<ApplicationUser> userManager, ITokenService tokenService, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("update-name")]
        [Authorize]
        public async Task<ActionResult> UpdateName(ChangeNameDTO profileDTO)
        {
            var username = User.GetUserName();
            var user = await _userManager.FindByNameAsync(username);

            var res = await _userManager.CheckPasswordAsync(user, profileDTO.Password);

            if (res == false)
            {
                return BadRequest("Inavalid Password");
            }

            if (profileDTO.Name == null || profileDTO.Name == "")
            {
                return BadRequest("Empty Name Field");
            }

            user.Name = profileDTO.Name;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return StatusCode(500, result.Errors);
            }

            await _signInManager.CheckPasswordSignInAsync(user, profileDTO.Password, false);
            var userLogin = new UserAuthResDTO
            {
                Name = user.Name,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };

            return Ok(userLogin);
        }
        [HttpPost("change-password")]
        [Authorize]
        public async Task<ActionResult> ChangePassword(ChangePasswordDTO profileDTO)
        {
            var username = User.GetUserName();
            var user = await _userManager.FindByNameAsync(username);

            var res = await _userManager.ChangePasswordAsync(user, profileDTO.OldPassword, profileDTO.NewPassword);
            if (!res.Succeeded)
            {
                return StatusCode(500, res.Errors);
            }
            await _signInManager.CheckPasswordSignInAsync(user, profileDTO.NewPassword, false);
            var userLogin = new UserAuthResDTO
            {
                Name = user.Name,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };

            return Ok(userLogin);
        }
    }
}
