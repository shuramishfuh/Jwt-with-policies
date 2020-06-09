using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using FoodAppAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FoodAppAPI.Controllers
{ 
    [Route("api/[controller]")]
   [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpGet]
        public ActionResult get()
        {
            return Ok(appUsers);
        }
         private readonly IConfiguration _config;    
        // mock user
    
        private List<User> appUsers = new List<User>    
        {    
            new User {  Name = "Admin",  UserName = "admin", Password = "1234", Role = "Admin" },    
            new User {  Name = "user",  UserName = "user", Password = "1234", Role = "user" },    
            new User {  Name = "Delivery",  UserName = "Delivery", Password = "1234", Role = "Delivery" },    
             
        };    
    
        public LoginController(IConfiguration config)    
        {    
            _config = config;    
        }    
        
        [HttpPost]
        public ActionResult Login(User login)    
        {
            User user = AuthenticateUser(login);    
            if (user != null)    
            {    
                var tokenString = GenerateJwt(user);    
                return  Ok(new    
                {    
                    token = tokenString,    
                    userDetails = user,    
                });    
            }    
            return StatusCode(401);    
        }    
    // login method
        User AuthenticateUser(User loginCredentials)    
        {    
            User user = appUsers.SingleOrDefault(x => x.UserName == loginCredentials.UserName && x.Password == loginCredentials.Password);    
            return user;    
        }    
    
        string GenerateJwt(User userInfo)    
        {    
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));    
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);    
    
            var claims = new[]    
            {    
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),    
                new Claim("UserName", userInfo.UserName),    
                new Claim("role",userInfo.Role),    
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),    
            };    
    
            var token = new JwtSecurityToken(    
                issuer: _config["Jwt:Issuer"],    
                audience: _config["Jwt:Audience"],    
                claims: claims,    
                expires: DateTime.Now.AddMinutes(30),    
                signingCredentials: credentials    
            );    
            return new JwtSecurityTokenHandler().WriteToken(token);    
        }    
      
    }
}
