using Gradution_Project.DTOs;
using Gradution_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Google.Apis.Auth;

namespace Gradution_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        // ================= REGISTER =================
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return BadRequest("User already exists");

            var user = new ApplicationUser
            {
                FullName = model.FullName,
                UserName = model.Email,
                Email = model.Email

            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Add Default Role (User)
            await _userManager.AddToRoleAsync(user, "User");

            return Ok("User registered successfully");

        }


        // ================= LOGIN =================
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Unauthorized("Invalid credentials");

            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                model.Password,
                false
            );

            if (!result.Succeeded)
                return Unauthorized("Invalid credentials");

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.First();

            var token = GenerateToken(user, role);

            return Ok(new AuthResponseDto
            {
                Token = token,
                Role = role
            });
        }

        // ================ admin login =================

        [HttpPost("admin/login")]
        public async Task<IActionResult> AdminLogin(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return Unauthorized("Invalid credentials");

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains("Admin"))
                return Unauthorized("Access denied");

            var result = await _signInManager.CheckPasswordSignInAsync(
                user, dto.Password, false);

            if (!result.Succeeded)
                return Unauthorized("Invalid credentials");

            var token = GenerateToken(user, "Admin");

            return Ok(new AuthResponseDto
            {
                Token = token,
                Role = "Admin"
            });
        }








        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return Ok(); // ❗ Security: متقولش إن الإيميل مش موجود

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetLink = $"http://localhost:3000/reset-password?email={user.Email}&token={Uri.EscapeDataString(token)}";

            // هنا تبعت Email
            // emailService.Send(user.Email, resetLink);

            return Ok("Reset link sent");
        }



        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            if (dto.NewPassword != dto.ConfirmPassword)
                return BadRequest("Passwords do not match");

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return BadRequest("Invalid request");

            var result = await _userManager.ResetPasswordAsync(
                user,
                dto.Token,
                dto.NewPassword
            );

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Password reset successfully");
        }






        /*
         * {
  "fullName": "mohamed abd elnasser",
  "email": "mo@gmail.com",
  "password": "Mohamed123#",
  "confirmPassword": "Mohamed123#"
}
         */

        // =================


        [Authorize(Roles = "Admin")]
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(CreateUserDto dto)
        {
            var user = new ApplicationUser
            {
                FullName = dto.FullName,
                Email = dto.Email,
                UserName = dto.Email
            };
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // ✅ دي أهم سطر
            await _userManager.AddToRoleAsync(user, dto.Role);

            return Ok("User created successfully");
        }






        // gmail login 


        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginDto dto)
        {
            GoogleJsonWebSignature.Payload payload;

            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(dto.IdToken);
            }
            catch
            {
                return Unauthorized("Invalid Google token");
            }

            var user = await _userManager.FindByEmailAsync(payload.Email);

            if (user == null)
            {
                user = new ApplicationUser
                {
                    Email = payload.Email,
                    UserName = payload.Email,
                    FullName = payload.Name
                };

                await _userManager.CreateAsync(user);
                await _userManager.AddToRoleAsync(user, "User");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.First();

            var token = GenerateToken(user, role);

            return Ok(new AuthResponseDto
            {
                Token = token,
                Role = role
            });
        }










        // ================= JWT =================



        private string GenerateToken(ApplicationUser user, string role)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, role)
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["JWT:Key"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //[HttpPost("create-role")]
        //public async Task<IActionResult> CreateRole(string roleName)
        //{
        //    var role = new IdentityRole(roleName);
        //    await _roleManager.CreateAsync(role);
        //    return Ok();
        //}

    }
}
