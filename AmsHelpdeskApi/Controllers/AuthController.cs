using Microsoft.AspNetCore.Mvc;
using AmsHelpdeskApi.Data;
using AmsHelpdeskApi.Models;
using AmsHelpdeskApi.Services;

namespace AmsHelpdeskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        private readonly PasswordService _passwordService;

        public AuthController(AppDbContext context, IConfiguration config, PasswordService passwordService)
        {
            _context = context;
            _config = config;
            _passwordService = passwordService;
        }

        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);

            if (existingUser != null)
            {
                return BadRequest(new { message = "Usuário já existe" });
            }

            if (string.IsNullOrEmpty(user.Role))
            {
                user.Role = "User";
            }

            user.PasswordHash = _passwordService.HashPassword(user.PasswordHash);

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login(User login)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == login.Email);
            

            if (user == null)
            {
                return Unauthorized(new { message = "Email ou senha inválidos" });
            }

            var validPassword = _passwordService.VerifyPassword(login.PasswordHash, user.PasswordHash);

            if(!validPassword)
            {
                return Unauthorized(new { message = "Email ou senha inválidos" });
            }

            var tokenService = new TokenService();
            var token = tokenService.GenerateToken(user.Email, user.Role, user.Id, _config["Jwt:Key"]);

            return Ok(new { token });
        }
    }
}
