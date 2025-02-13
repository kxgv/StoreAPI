using Microsoft.AspNetCore.Mvc;
using StoreAPI.Core.Interfaces;

namespace StoreAPI.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IAuthService _authService;

        public AuthController(
            ILogger<ProductsController> logger,
            IAuthService authService)
        {
            _logger = logger;
            _authService = authService; 
        }

        // POST: api/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _authService.AuthenticateAsync(request.Email, request.Password);
            if (token == null)
                return Unauthorized(new { message = "Credenciales incorrectas" });

            return Ok(new { Token = token });
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
