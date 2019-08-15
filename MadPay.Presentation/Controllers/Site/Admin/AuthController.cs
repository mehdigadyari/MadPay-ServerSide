using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MadPay.Common.ErrorAndMessage;
using MadPay.Data.DatabaseContext;
using MadPay.Data.Dtos.Site.Admin;
using MadPay.Data.Models;
using MadPay.Repo.Infrastructure;
using MadPay.Services.Site.Admin.Auth.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MadPay.Presentation.Controllers.Site.Admin
{

    [Authorize]
    [Route("site/admin/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork<MadpayDbContext> _db;
        private readonly IAuthService _authService;
        private readonly IConfiguration _config;
        public AuthController(IUnitOfWork<MadpayDbContext> dbContext, IAuthService authService, IConfiguration config)
        {
            _db = dbContext;
            _authService = authService;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.UserName = userForRegisterDto.UserName.ToLower();
            if (await _db.UserRepository.UserExists(userForRegisterDto.UserName))
                return BadRequest(new ReturnMessage()
                {
                    status = false,
                    title = "خطا",
                    message = "نام کاربری وجود دارد"
                });

            var userToCreate = new User
            {
                UserName = userForRegisterDto.UserName,
                Name = userForRegisterDto.Name,
                PhoneNumber = userForRegisterDto.PhoneNumber,
                Address = "",
                City = "",
                Gender = true,
                DateOfBirth = DateTime.Now,
                IsAcive = true,
                Status = true
            };

            var createdUser = await _authService.Register(userToCreate, userForRegisterDto.Password);

            return StatusCode(201);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UseForLoginDto useForLoginDto)
        {
            var userFromRepo = await _authService.Login(useForLoginDto.UserName, useForLoginDto.Password);

            if (userFromRepo == null)
                return Unauthorized(new ReturnMessage()
                {
                    status = false,
                    title = "خطا",
                    message = "کاربری با این یوزر و پس وجود ندارد"
                });

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name,userFromRepo.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDes = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = useForLoginDto.IsRemember ? DateTime.Now.AddDays(1) : DateTime.Now.AddHours(2),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDes);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });

        }



        [AllowAnonymous]
        [HttpGet("GetValue")]
        public async Task<IActionResult> GetValue()
        {
            return Ok(new ReturnMessage()
            {
                status = true,
                title = "اوکی",
                message = ""
            });
        }

        [HttpGet("GetValues")]
        public async Task<IActionResult> GetValues()
        {
            return Ok(new ReturnMessage()
            {
                status = true,
                title = "اوکی",
                message = ""
            });
        }
    }
}