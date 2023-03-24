using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using API.Data;
using System.Security.Cryptography;
using System.Text;
using API.DTOs;
using API.Interfaces;
using API.Services;

namespace API.Controllers
{

    public class AccountController: BaseApiController
    {
        
         //public AccountController(DataContext context): base(context) {}

        private readonly DataContext _context;
        private ITokenService _tokenService;

        
        public AccountController(DataContext context, ITokenService tokenService) : base(context){
            _context = context;
            _tokenService = tokenService;
        }

        

        [HttpPost("register")]
         public async Task<ActionResult<UserDto>> Register(RegisterDto userDto)
         {

            if(await UserExists(userDto.Username)) return BadRequest("Usuário já existe.");

            using var hmac = new HMACSHA512();

            var user = new AppUser{
                UserName = userDto.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return new UserDto{
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };

         }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto){

            // if(string.IsNullOrWhiteSpace(loginDto.Username) || string.IsNullOrWhiteSpace(loginDto.Password))
            //     return BadRequest("Os campos usuário e senha são obrigatórios.");
            
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username);

            if(user == null) return Unauthorized("Login inválido");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for(var i=0; i<computedHash.Length; i++){
                if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Login inválido");
            }

            return new UserDto{
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }


         private async Task<bool> UserExists(string username){
               return await _context.Users.AnyAsync(x => x.UserName.Equals(username));
         }

    }
}