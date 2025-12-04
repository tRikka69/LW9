using Microsoft.AspNetCore.Mvc;
using AmateurTheaterMongo.Models;
using AmateurTheaterMongo.Services;
using AmateurTheaterMongo.Helpers;
using AmateurTheaterMongo.DTO;

namespace AmateurTheaterMongo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly PasswordHasher _passwordHasher;

        public AuthController(IUserService userService, JwtTokenGenerator jwtGenerator, PasswordHasher hasher)
        {
            _userService = userService;
            _jwtTokenGenerator = jwtGenerator;
            _passwordHasher = hasher;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto request)
        {
            var existingUser = await _userService.FindUserAsync(request.Email);
            if (existingUser != null)
                return BadRequest("Користувач з таким Email вже існує.");

            var newUser = new User
            {
                Email = request.Email,
                PasswordHash = _passwordHasher.HashPassword(request.Password),
                Role = "User"
            };

            await _userService.RegisterUserAsync(newUser);
            return Ok("Користувач успішно зареєстрований.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto request)
        {
            var user = await _userService.FindUserAsync(request.Email);
            if (user == null)
                return Unauthorized("Невірний Email або пароль.");

            if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
                return Unauthorized("Невірний Email або пароль.");

            var token = _jwtTokenGenerator.Generate(user);
            return Ok(new { Token = token });
        }
    }
}