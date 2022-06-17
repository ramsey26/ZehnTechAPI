using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
      
        private readonly DataContext _dataContext;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext dataContext,ITokenService tokenService)
        {
            _tokenService = tokenService;
            _dataContext = dataContext;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDto>> Register(RegisterDto registerDto){
            
            var user = await _dataContext.AppUsers.AnyAsync(x=>x.Email==registerDto.Email);

            if(user){
                return BadRequest("Email is taken");
            }

            using var hmac = new HMACSHA512();

            var appUser = new AppUser{
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key,
                Email = registerDto.Email
            };

            _dataContext.AppUsers.Add(appUser);

            await _dataContext.SaveChangesAsync();

            return new UserResponseDto{
                Email = appUser.Email,
                Token = _tokenService.CreateToken(appUser)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserResponseDto>> Login(LoginDto loginDto){

            var user = await _dataContext.AppUsers.SingleOrDefaultAsync(x=>x.Email == loginDto.Email);

            if(user == null) return Unauthorized("Invalid email");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            
            for(int i =0;i<computedHash.Length;i++){
                if(computedHash[i]!=user.PasswordHash[i]) return Unauthorized("Invalid password");
            }

            return new UserResponseDto{
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }

        [Authorize]
        [HttpGet("get-user")]
        public async Task<ActionResult<AppUser>> GetLoggedInUser(){


           var email = User.FindFirst(ClaimTypes.NameIdentifier).Value;
           var appUser = await _dataContext.AppUsers.SingleOrDefaultAsync(x=>x.Email == email);

            if(appUser!=null) return Ok(appUser);
            return BadRequest("User not found"); 
        }
    }
}