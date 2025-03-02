using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StoreAPI.Core.Interfaces;
using static StoreAPI.Infraestructure.EntityFramework.Context.ApplicationDbContext;

namespace StoreAPI.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ProductsController> _logger;
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public AuthController(
            ILogger<ProductsController> logger,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IAuthService authService,
            IConfiguration configuration)
        {
            _logger = logger;
            _authService = authService;
            _signInManager = signInManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // POST: api/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email, IsAdmin = model.IsAdmin};
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { message = "Usuario registrado correctamente" });
        }

        // POST: api/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            _logger.LogDebug("{MethodName}", nameof(Login));
            if (model == null)
            {
                return BadRequest("El modelo de solicitud no puede ser nulo.");
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return Unauthorized("User not found");

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!result.Succeeded) return Unauthorized("Wrong password");

            try
            {
                var token = GenerateJwtToken(user);
                return Ok(new { token });
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            _logger.LogDebug("{MethodName}", nameof(GenerateJwtToken));
            if (string.IsNullOrEmpty(user.Email))
            {
                throw new ArgumentException("Email is null");
            }
            if (string.IsNullOrEmpty(_configuration["Jwt:Key"]))
            {
                throw new NullReferenceException("No JWT key, revise Config");
            }

            byte[] key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("IsAdmin", user.IsAdmin.ToString()) // Add IsAdmin claim
                };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

    public class LoginRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class RegisterModel
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public bool IsAdmin {  get; set; }
    }