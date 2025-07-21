using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment.Data;
using Assignment.Models;
using Assignment.Service;

namespace Assignment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IAuthService _auth;

        public AuthController(ApplicationDbContext db, IAuthService auth)
        {
            _db = db;
            _auth = auth;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthRequest request)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user == null) return Unauthorized("Invalid username or password.");

            if (!_auth.VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
                return Unauthorized("Invalid username or password.");

            var token = _auth.GenerateJwtToken(user);
            return Ok(new { token, role = user.Role });
        }
    }

}
