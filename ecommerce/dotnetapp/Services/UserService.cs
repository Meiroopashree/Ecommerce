using dotnetapp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using dotnetapp.Data;

namespace dotnetapp.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public UserService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;
        }

        public async Task<bool> RegisterAsync(User user)
{
    try
    {
        Console.WriteLine($"Attempting to register user with email: {user.EmailID}");
        var userExists = await _userManager.FindByEmailAsync(user.EmailID);
        
        if (userExists != null)
        {
            Console.WriteLine($"User with email '{user.EmailID}' already exists.");
            return false;
        }

        var identityUser = new IdentityUser
        {
            UserName = user.UserName,
            Email = user.EmailID
        };

        var result = await _userManager.CreateAsync(identityUser, user.Password);

        if (result.Succeeded)
        {
            var roleResult = await _userManager.AddToRoleAsync(identityUser, user.UserRole);
            if (roleResult.Succeeded)
            {
                Console.WriteLine($"User '{user.EmailID}' successfully registered with role '{user.UserRole}'.");
                return true;
            }
            else
            {
                Console.WriteLine($"Failed to add role '{user.UserRole}' to user '{user.EmailID}'. Errors:");
                foreach (var error in roleResult.Errors)
                {
                    Console.WriteLine($"- {error.Description}");
                }
                return false;
            }
        }
        else
        {
            Console.WriteLine($"Registration failed for user with email '{user.EmailID}'. Errors:");
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"- {error.Description}");
            }
            return false;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception during registration: {ex.Message}");
        return false;
    }
}

        public async Task<string> LoginAsync(string email, string password)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
{
    Console.WriteLine($"User with email '{email}' not found.");
    return null;
}

                var signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);
if (!signInResult.Succeeded)
{
     Console.WriteLine("Password check failed.");
    Console.WriteLine($"Error: {signInResult.ToString()}");
    return null;
}

                Console.WriteLine("Before generating token");
var token = GenerateJwtToken(user);
Console.WriteLine("After generating token");
Console.WriteLine("Token: " + token);

                Console.WriteLine("Token: " + token);

                return token;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
                //new Claim(ClaimTypes.Email, user.Email),
            };
Console.WriteLine("GenerateToken -"+user.UserName);
Console.WriteLine("GenerateToken -"+user.Email);
            var roles = _userManager.GetRolesAsync(user).Result;

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
