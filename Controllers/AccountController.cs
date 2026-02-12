using BookStore.Models.DTOs.Account;
using BookStore.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO regDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (string.IsNullOrEmpty(regDTO.Password))
                    return BadRequest("Введите пароль.");

                var appUser = new AppUser
                {
                    UserName = regDTO.UserName,
                    Email = regDTO.Email
                };
                var createdUser = await _userManager.CreateAsync(appUser, regDTO.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User"); // если создание пользователя успешно, присваиваем любому зарегистрировавшемуся роль User
                    if (roleResult.Succeeded)
                    {
                        return Ok("Пользователь создан");
                    } else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}
