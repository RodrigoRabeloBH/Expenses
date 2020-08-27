using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Api.Helpers;
using AutoMapper;
using Domain;
using Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _rep;
        private readonly IMapper _mapper;
        private readonly IConfiguration _conf;

        public UserController(IUserServices rep, IMapper mapper, IConfiguration conf)
        {
            _rep = rep;
            _mapper = mapper;
            _conf = conf;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserToCreate userToCreate)
        {
            userToCreate.Username = userToCreate.Username.ToUpper();

            if (await _rep.UserExistes(userToCreate.Username)) return BadRequest("Username alread exists");

            var user = await _rep.Register(_mapper.Map<User>(userToCreate));

            return Ok(_mapper.Map<UserToReturn>(user));
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserToLogin userToLogin)
        {
            var userFromLogin = await _rep.Login(userToLogin);

            if (userFromLogin == null)
            {

                return Unauthorized();
            }
            else
            {
                var claims = new[]
                 {
                    new Claim(ClaimTypes.NameIdentifier, userFromLogin.Id.ToString()),
                    new Claim(ClaimTypes.Name, userFromLogin.Username)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(_conf.GetSection("AppSettings:Token").Value));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptior = new SecurityTokenDescriptor
                {

                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptior);


                return Ok(new
                {
                    user = _mapper.Map<UserToReturn>(userFromLogin),
                    token = tokenHandler.WriteToken(token)
                });
            }
        }
        [HttpGet("{index}/{length}")]
        public async Task<IActionResult> GetAll(int index, int length)
        {
            var users = _mapper.Map<IEnumerable<UserToReturn>>(await _rep.GetAll());

            return Ok(new Pagination<UserToReturn>(users, index, length));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(_mapper.Map<UserToReturn>(await _rep.GetById(id)));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(UserToUpdate userToUpdate, int id)
        {
            var user = await _rep.GetById(userToUpdate.Id);

            await _rep.Update(_mapper.Map(userToUpdate, user));

            return Ok(_mapper.Map<UserToReturn>(user));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _rep.Delete(id);
            return Ok();
        }
    }
}