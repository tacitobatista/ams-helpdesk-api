using AmsHelpdeskApi.Domain.Entities;
using AmsHelpdeskApi.DTOs.Auth;
using AmsHelpdeskApi.Infrastructure.Data;
using AmsHelpdeskApi.Services;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;

namespace AmsHelpdeskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        private readonly PasswordService _passwordService;
        private readonly TokenService _tokenService;

        public AuthController(AppDbContext context, IConfiguration config, PasswordService passwordService, TokenService tokenService)
        {
            _context = context;
            _config = config;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequestDto request)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == request.Email);

            if (existingUser != null)
            {
                return BadRequest(new { message = "Usuário já existe" });
            }

            var user = new User
            {
                Email = request.Email,
                Role = "User"
            };

            user.PasswordHash = _passwordService.HashPassword(user, request.Password);

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequestDto request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);
            

            if (user == null)
            {
                return Unauthorized(new { message = "Email ou senha inválidos" });
            }

            var validPassword = _passwordService.VerifyPassword(user, request.Password);

            if(!validPassword)
            {
                return Unauthorized(new { message = "Email ou senha inválidos" });
            }

            var token = _tokenService.GenerateToken(user);

            return Ok(new { token });
        }
    }
}
