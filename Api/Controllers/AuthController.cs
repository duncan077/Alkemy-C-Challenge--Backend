using AutoMapper;
using DisneyApi.Model;
using DisneyApi.Model.DTO.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DisneyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly UserManager<DisneyUser> _userManager;
        private readonly IConfiguration configuration;

        public AuthController(ILogger<AuthController> logger,  UserManager<DisneyUser> userManager, IConfiguration configuration)
        {
            _logger = logger;
            _userManager = userManager;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginDTO loginDTO)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(loginDTO.UserName);
                var passwordValid = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
                if (user == null || !passwordValid)
                    return Unauthorized(loginDTO);
                string tokenStr = await GenerateToken(user);
                var response = new AuthResponse
                {
                    UserName = loginDTO.UserName,
                    Token = tokenStr,
                    UserId = user.Id,
                    IsAuthSuccessful = true
                };
                _logger.LogInformation("User Login Successful");
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went worng");
                return Problem("Login error", statusCode: 500);
            }
        }
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<AuthResponse>> Register(RegisterDTO registerDTO)
        {
            try
            {
                var user = new DisneyUser();
                user.UserName = registerDTO.UserName;
                user.Email = registerDTO.Email;
                var result = await _userManager.CreateAsync(user, registerDTO.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }
                
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went worng");
                return Problem("Register error", statusCode: 500);
            }
        }
        private async Task<string> GenerateToken(DisneyUser user)
        {
            try
            {
                var securitySecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Secret"]));
                var credentials = new SigningCredentials(securitySecret, SecurityAlgorithms.HmacSha256);
                var roles = await _userManager.GetRolesAsync(user);

                var rolesClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();
                var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name,user.UserName),
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("uid", user.Id)
            }.Union(rolesClaims);

                var token = new JwtSecurityToken(
                    issuer: configuration["JwtSettings:Issuer"],
                    audience: configuration["JwtSettings:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(int.Parse(configuration["JwtSettings:Duration"])),
                    signingCredentials: credentials);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
