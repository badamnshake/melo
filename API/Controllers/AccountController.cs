using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System.Text;
using API.DTOs;
using API.Interfaces;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _ctx;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext ctx, ITokenService tokenService)
        {
            _ctx = ctx;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username!)) return BadRequest("username is taken");
            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                Username = registerDto.Username!.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password!)),
                PasswordSalt = hmac.Key
            };

            _ctx.Users.Add(user);
            await _ctx.SaveChangesAsync();
            return new UserDto
            {
                Username = user.Username,
                Token = _tokenService.CreateToken(user)
            };


        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> login(LoginDto loginDto)
        {
            var user = await _ctx.Users.SingleOrDefaultAsync(x => x.Username == loginDto.Username);

            if (user == null) return Unauthorized("Invalid Username");

            using var hmac = new HMACSHA512(user.PasswordSalt!);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password!));

            if (computedHash.SequenceEqual(user.PasswordHash!))
            {
                return new UserDto
                {
                    Username = user.Username,
                    Token = _tokenService.CreateToken(user)
                };
            }

            return Unauthorized("Invalid Username");



        }
        private async Task<bool> UserExists(string username)
        {
            return await _ctx.Users.AnyAsync(x => x.Username == username.ToLower());
        }
    }
}