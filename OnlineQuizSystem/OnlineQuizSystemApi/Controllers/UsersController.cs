using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineQuizSystemApi.DTOs;
using OnlineQuizSystemApi.Logging;
using OnlineQuizSystemApi.Models;
using OnlineQuizSystemApi.Security;

namespace OnlineQuizSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly OnlineQuizSystemContext _context;
        private readonly LoggerBase _consoleLogger;
        private readonly LoggerBase _fileLogger;
        private readonly string path  = "C:\\Users\\Tin\\Desktop\\online-quiz-system\\OnlineQuizSystem\\logs.txt";

        public UsersController(OnlineQuizSystemContext context)
        {
            _context = context;
            _consoleLogger = Logging.LoggerFactory.CreateLogger("Console");
            _fileLogger = Logging.LoggerFactory.CreateLogger("File", true, path);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var existingUser = await _context.Users.AnyAsync(u => u.Email == registerDto.Email);
            if (existingUser)
            {
                return BadRequest("Email is already registered.");
            }

            var roleExists = await _context.Roles.AnyAsync(r => r.Id == registerDto.RoleId);
            if (!roleExists)
            {
                return BadRequest("Invalid RoleId.");
            }

            var passwordHash = PasswordHelper.HashPassword(registerDto.Password, out var salt);

            var user = new User
            {
                Email = registerDto.Email,
                PwdHash = passwordHash,
                PwdSalt = salt,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                RoleId = registerDto.RoleId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _consoleLogger.Log("Registration successful.");
            _fileLogger.Log("Registration successful.");
            return Ok("User registered successfully!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }


            var isPasswordValid = PasswordHelper.VerifyPassword(loginDto.Password, user.PwdHash, user.PwdSalt);
            if (!isPasswordValid)
            {
                _fileLogger.Log("Login failed.");
                _consoleLogger.Log("Login failed.");
                return Unauthorized("Invalid email or password.");
            }

            _consoleLogger.Log("Login successful.");
            _fileLogger.Log("Login successful.");
            return Ok(new { Message = "Login successful!" });
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
