using System.Security.Claims;
using API.Contracts.Auth;
using API.Contracts.User;
using API.Services.Auth;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(UserManager<AppUser> userManager, TokenService tokenService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<AuthUserDto>> Login(LoginDto loginDto)
    {
        var user = await userManager.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Email == loginDto.Email);

        if (user == null) return Unauthorized();

        var result = await userManager.CheckPasswordAsync(user, loginDto.Password);

        if (result) return CreateUserObject(user);

        return Unauthorized();
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<AuthUserDto>> Register(RegisterDto registerDto)
    {
        if (await userManager.Users.AnyAsync(x => x.UserName == registerDto.Username))
        {
            ModelState.AddModelError("username", "Username taken");
            return ValidationProblem();
        }


        if (await userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
        {
            ModelState.AddModelError("email", "Email taken");
            return ValidationProblem();
        }


        var user = new AppUser
        {
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email,
            UserName = registerDto.Username
        };

        var result = await userManager.CreateAsync(user, registerDto.Password);

        if (result.Succeeded) return CreateUserObject(user);

        return BadRequest(result.Errors);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<AuthUserDto>> GetCurrentUser()
    {
        var user = await userManager.Users.Include(p => p.Photos)
            .FirstOrDefaultAsync(u => u.Email == User.FindFirstValue(ClaimTypes.Email));

        return CreateUserObject(user);
    }

    private AuthUserDto CreateUserObject(AppUser appUser)
    {
        return new AuthUserDto
        {
            DisplayName = appUser.DisplayName,
            Image = appUser.Photos?.FirstOrDefault(x => x.IsMain)?.Url,
            Token = tokenService.CreateToken(appUser),
            Username = appUser.UserName
        };
    }
}
